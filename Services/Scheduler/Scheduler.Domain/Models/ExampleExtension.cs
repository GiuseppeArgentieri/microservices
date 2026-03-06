namespace Scheduler.Domain.Models;

public class ExampleExtension: Entity<ExampleExtensionId>
{
    private ExampleExtension() { }
    internal ExampleExtension(ExampleId exId, ExampleName exName, int quantity)
    {
        Id = ExampleExtensionId.Of(Guid.NewGuid());
        ExampleName = exName;
        Quantity = quantity;
        ExampleId = exId;
    }
    public ExampleName ExampleName { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public ExampleId ExampleId { get; private set; } = default!;
}
