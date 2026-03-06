using MassTransit.Transports;
namespace Scheduler.Infrastructure.Data.Extensions;

internal class InitialData
{
   
    public static IEnumerable<Example> ExampleWithItems
    {
        get
        {
            var guid1 = Guid.NewGuid();
            var order1 = Example.Create(
                            ExampleId.Of(guid1),
                            Guid.NewGuid(),
                            "ord_1");
            order1.Add(ExampleId.Of(guid1), ExampleName.Of("xxx"), 24);
            order1.Add(ExampleId.Of(guid1), ExampleName.Of("fff"), 23);
            order1.Add(ExampleId.Of(guid1), ExampleName.Of("aaa"), 22);
            var guid2 = Guid.NewGuid();
            var order2 = Example.Create(
                            ExampleId.Of(guid2),
                            Guid.NewGuid(),
                            "ord_2");
            order2.Add(ExampleId.Of(guid2), ExampleName.Of("Uff"), 44);
            order2.Add(ExampleId.Of(guid2), ExampleName.Of("sss"), 42);

            return new List<Example> { order1, order2 };
        }
    }
}

