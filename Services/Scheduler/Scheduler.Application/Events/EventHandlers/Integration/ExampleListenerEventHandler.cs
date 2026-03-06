using BuildingBlocks.Messaging.Events;
using MassTransit;
using Scheduler.Application.Dtos;
using Scheduler.Application.Events.Commands.ExampleEvent;

namespace Scheduler.Application.Events.EventHandlers.Integration;

public class ExampleListenerEventHandler
    (ISender sender, ILogger<ExampleListenerEventHandler> logger)
    : IConsumer<ExampleListenerEvent>
{
    // Expected payload
    //    {
    //  "messageId": "11111111-1111-1111-1111-111111111111",
    //  "messageType": [
    //    "urn:message:BuildingBlocks.Messaging.Events:ExampleListenerEvent"
    //  ],
    //  "message": {
    //    "id": "6fa85f64-5717-4562-b3fc-2c963f66afa6",
    //    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    //    "orderName": "string"
    //  }
    //}
    public async Task Consume(ConsumeContext<ExampleListenerEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private ExampleEventCommand MapToCreateOrderCommand(ExampleListenerEvent message)
    {
        // Create full order with incoming event data

        var orderDto = new ExampleDto(
            Id: message.Id,
            CustomerId: message.CustomerId,
            OrderName: message.OrderName);

        return new ExampleEventCommand(orderDto);
    }
}
