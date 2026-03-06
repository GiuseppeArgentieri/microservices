namespace Scheduler.Domain.ValueObjects;

public record ExampleExtensionId
{
    public Guid Value { get; }
    private ExampleExtensionId(Guid value) => Value = value;
    public static ExampleExtensionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty.");
        }

        return new ExampleExtensionId(value);
    }
}