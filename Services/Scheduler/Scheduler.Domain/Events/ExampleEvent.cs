namespace Scheduler.Domain.Events;
public record ExampleEvent(Example example) : IDomainEvent;
public record ExampleUpdatedEvent(Example example) : IDomainEvent;
