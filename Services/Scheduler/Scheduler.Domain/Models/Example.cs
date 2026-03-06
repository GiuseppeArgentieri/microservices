using Scheduler.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Domain.Models;

public class Example : Aggregate<ExampleId>
{
    public Guid CustomerId { get; private set; }
    public string OrderName { get; private set; } = default!;
    private readonly List<ExampleExtension> _exampleItems = new();
    public IReadOnlyList<ExampleExtension> ExampleItems => _exampleItems.AsReadOnly();

    public static Example Create(ExampleId id, Guid customerId, string orderName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderName);

        var example = new Example
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName
        };

        example.AddDomainEvent(new ExampleEvent(example));

        return example;
    }

    public void Update(Guid customerId, string orderName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderName);

        CustomerId = customerId;
        OrderName = orderName;

        AddDomainEvent(new ExampleUpdatedEvent(this));
    }

    // ExampleExtension(ExampleId exId, ExampleName exName, int quantity)
    public void Add(ExampleId exId, ExampleName exName, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var extitem = new ExampleExtension(exId, exName, quantity);
        _exampleItems.Add(extitem);
    }
}
