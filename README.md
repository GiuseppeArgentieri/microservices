# High-Performance Open-Source Alternative to MediatR with CQRS & DDD

## Why This Repository?

[MediatR](https://github.com/jbogard/MediatR), the de facto standard for implementing the **Mediator pattern** in .NET, has transitioned to a **commercial licensing model**, requiring teams to purchase a license for production use.

To address this, this project adopts a fully open-source alternative stack:

- [`Mediator.Abstractions`](https://github.com/martinothamar/Mediator): provides the core mediator abstractions
- [`Mediator.SourceGenerator`](https://github.com/martinothamar/Mediator): leverages C# source generators to produce efficient, compile-time mediator wiring with zero runtime reflection overhead

This shift not only eliminates the licensing concern but also results in **better performance** compared to MediatR, thanks to source generation.

---

## Architecture Overview

This repository is designed to demonstrate a **production-ready implementation** of:

- ✅ **Domain-Driven Design (DDD)**: rich domain models, aggregates, value objects, and domain events
- ✅ **CQRS (Command Query Responsibility Segregation)**: strict separation between write (commands) and read (queries) paths
- ✅ **Clean Architecture**: clear boundaries between Domain, Application, Infrastructure, and Presentation layers
- ✅ **Outbox / Event Dispatch via RabbitMQ**: domain events are automatically published after persistence

---

## How It Works: Example Endpoint

### Request

```bash
curl -X 'POST' \
  'https://localhost:55883/example' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "exampleDto": {
    "id": "9fa85f64-5717-4562-b3fc-2c963f66afa6",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "orderName": "string"
  }
}'
```

### Behind the Scenes

When this `POST` request is executed, the following pipeline is triggered:

```
HTTP Request
    │
    ▼
Controller  ──►  Command  ──►  Command Handler
                                    │
                                    ▼
                             Domain Aggregate
                           (raises Domain Event)
                                    │
                                    ▼
                             DbContext.SaveChangesAsync()
                                    │
                                    ▼
                    ┌───────────────────────────────┐
                    │   Infrastructure Interceptor   │
                    │  (SavingChangesAsync override) │
                    └───────────────────────────────┘
                                    │
                                    ▼
                       Domain Event Dispatcher
                                    │
                                    ▼
                           RabbitMQ Publisher
                         (event published to broker)
```

### The Interceptor Pattern

At the infrastructure layer, a **custom EF Core `SaveChangesInterceptor`** is registered. It overrides `SavingChangesAsync` so that, just before changes are committed to the database, all **domain events raised by aggregates** are automatically dispatched.

This ensures:

- Domain events are **always published in sync with persistence**: no manual dispatch required in application handlers
- The domain model stays **clean and decoupled** from infrastructure concerns
- Event publishing is **consistent and centralized**, reducing the risk of missed events

---

## RabbitMQ Consumer - Inbound Event Handling

In addition to publishing events outbound, this project implements a **RabbitMQ consumer** that listens for incoming messages and triggers the creation of a new example aggregate in response.

### Exchange & Routing

The consumer is bound to the following exchange:

```
Exchange: BuildingBlocks.Messaging.Events:ExampleListenerEvent
```

### Expected Payload

Publishing the following message to the exchange will cause the consumer to handle the event and create a new example entry via the application layer:

```json
{
  "messageId": "11111111-1111-1111-1111-111111111111",
  "messageType": [
    "urn:message:BuildingBlocks.Messaging.Events:ExampleListenerEvent"
  ],
  "message": {
    "id": "5fa85f64-5717-4562-b3fc-2c963f66afa6",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "orderName": "string"
  }
}
```

### Consumer Pipeline

```
RabbitMQ Exchange
  (ExampleListenerEvent)
        │
        ▼
   Consumer Handler
        │
        ▼
  Deserialize Message
        │
        ▼
  Dispatch Command (via Mediator)
        │
        ▼
  Command Handler
        │
        ▼
  Domain Aggregate Created
        │
        ▼
  DbContext.SaveChangesAsync()
        │
        ▼
  Infrastructure Interceptor
  (Domain Events Dispatched)
```

This demonstrates a **full event-driven round-trip**: an external system publishes a message to RabbitMQ, which is consumed and handled entirely through the same CQRS + DDD pipeline used by the HTTP layer - keeping the architecture **consistent and symmetric** regardless of the entry point.

---

## Project Structure

```
src/
├── BuildingBlocks/
│   ├── BuildingBlock/
│   │   ├── Behaviors/
│   │   ├── CQRS/
│   │   ├── Exceptions/
│   │   │   └── Handler/
│   │   └── Pagination/
│   └── BuildingBlocks.Messaging/
│       ├── Events/
│       └── MassTransit/
└── Services/
    └── Scheduler/
        ├── Domain/                          # Aggregates, Entities, Value Objects, Domain Events
        │   ├── Abstractions/
        │   ├── Enums/
        │   ├── Events/
        │   ├── Exceptions/
        │   ├── Models/
        │   └── ValueObjects/
        ├── Application/                     # Commands, Queries, Handlers (via Mediator.SourceGenerator)
        │   ├── Data/
        │   ├── Dtos/
        │   ├── Events/
        │   │   ├── Commands/
        │   │   │   └── ExampleEvent/
        │   │   ├── EventHandlers/
        │   │   │   ├── Domain/
        │   │   │   └── Integration/
        │   │   └── Queries/
        │   │       └── GetExample/
        │   ├── Exceptions/
        │   └── Extensions/
        ├── Infrastructure/                  # EF Core, Interceptors, RabbitMQ Publisher
        │   └── Data/
        │       ├── Configurations/
        │       ├── Extensions/
        │       └── Interceptors/
        └── Presentation/                    # Controllers, DTOs, Minimal API endpoints
            ├── Endpoints/
            └── Properties/
```

---

## Technology Stack

| Concern | Library / Tool |
|---|---|
| Architecture | DDD + CQRS + Clean Architecture |
| API | ASP.NET Core + Carter |
| Mediator | `Mediator.Abstractions` + `Mediator.SourceGenerator` |
| ORM | Entity Framework Core 9 (SQL Server) |
| Message Broker | MassTransit + RabbitMQ |
| Mapping | Mapster |
| Validation | FluentValidation |
| Documentation | Swashbuckle (Swagger) |
| Health Checks | AspNetCore.HealthChecks (SqlServer + UI) |
| Identity | ASP.NET Core Identity |
| Containerization | Docker Compose (Linux) |

---

## License

This project is licensed under the [MIT License](LICENSE).
