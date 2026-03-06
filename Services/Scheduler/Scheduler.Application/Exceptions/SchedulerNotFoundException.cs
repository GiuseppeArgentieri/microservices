using BuildingBlocks.Exceptions;
namespace Scheduler.Application.Exceptions;

public class SchedulerNotFoundException : NotFoundException
{
    public SchedulerNotFoundException(Guid id) : base("Scheduler", id)
    {
    }
}
