using MassTransit.Transports;
using Scheduler.Application.Dtos;
using Scheduler.Domain.Models;

namespace Scheduler.Application.Extensions;

public static class ExampleExtensions
{
    public static IEnumerable<ExampleDto> ToExampleDtoList(this IEnumerable<Example> examples)
    {
        return examples.Select(example => new ExampleDto(
            Id: example.Id.Value,
            CustomerId: example.CustomerId,
            OrderName: example.OrderName
        ));
    }

    public static ExampleDto ToExampleDto(this Example example)
    {
        return DtoFromExample(example);
    }

    private static ExampleDto DtoFromExample(Example example)
    {
        return new ExampleDto(
                    Id: example.Id.Value,
                    CustomerId: example.CustomerId,
                    OrderName: example.OrderName
                );
    }
}

