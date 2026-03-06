namespace BuildingBlocks.Messaging.Events;

public record ExampleListenerEvent : IntegrationEvent
{
    public Guid Id { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public string OrderName { get; set; } = default!;
}
