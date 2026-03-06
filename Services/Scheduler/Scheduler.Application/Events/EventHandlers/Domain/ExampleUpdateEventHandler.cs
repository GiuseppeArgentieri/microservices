using MassTransit;
using Scheduler.Application.Extensions;
using Scheduler.Domain.Events;

namespace Scheduler.Application.Events.EventHandlers.Domain;

public class ExampleUpdateEventHandler(IPublishEndpoint publishEndpoint, ILogger<ExampleCreateEventHandler> logger)
    : INotificationHandler<ExampleUpdatedEvent>
{
    public async ValueTask Handle(ExampleUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var orderCreatedIntegrationEvent = domainEvent.example.ToExampleDto();
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
