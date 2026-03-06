using MassTransit;
using Scheduler.Application.Extensions;
using Scheduler.Domain.Events;

namespace Scheduler.Application.Events.EventHandlers.Domain;

public class ExampleCreateEventHandler
    (IPublishEndpoint publishEndpoint, ILogger<ExampleCreateEventHandler> logger)
    : INotificationHandler<ExampleEvent>
{
    public async ValueTask Handle(ExampleEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var orderCreatedIntegrationEvent = domainEvent.example.ToExampleDto();
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
