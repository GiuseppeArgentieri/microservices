using Scheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Example> Example { get; }
    DbSet<ExampleExtension> ExampleExtension { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
