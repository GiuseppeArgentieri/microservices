using Scheduler.Application.Data;
using Scheduler.Application.Dtos;
using Scheduler.Domain.Models;
using Scheduler.Domain.ValueObjects;

namespace Scheduler.Application.Events.Commands.ExampleEvent;
//internal class ExampleEventHandler
public class ExampleEventHandler(IApplicationDbContext dbContext)
    : IComandoHandler<ExampleEventCommand, ExampleEventResult>
{
    public async ValueTask<ExampleEventResult> Handle(ExampleEventCommand command, CancellationToken cancellationToken)
    {
        var example = CreateNewExample(command.exampleDto);

        dbContext.Example.Add(example);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ExampleEventResult(example.Id.Value);
    }

    private Example CreateNewExample(ExampleDto exDto)
    {
        var newExample = Example.Create(
                id: ExampleId.Of(exDto.Id),
                customerId: exDto.CustomerId,
                orderName: exDto.OrderName
                );

        return newExample;
    }
}


