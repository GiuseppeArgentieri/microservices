using Scheduler.Domain.Exceptions;

namespace Scheduler.Domain.ValueObjects;

public record ExampleId
{
    public Guid Value { get; }
    private ExampleId(Guid value) => Value = value;
    public static ExampleId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ExampleId cannot be empty.");
        }

        return new ExampleId(value);
    }
}
