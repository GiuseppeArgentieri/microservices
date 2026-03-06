using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Domain.ValueObjects;

public record ExampleName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private ExampleName(string value) => Value = value;
    public static ExampleName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new ExampleName(value);
    }
}