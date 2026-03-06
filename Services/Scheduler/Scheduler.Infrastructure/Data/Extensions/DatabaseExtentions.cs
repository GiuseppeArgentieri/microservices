using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Infrastructure.Data.Extensions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedExamplesAsync(context);
        // await Seed...
    }

    private static async Task SeedExamplesAsync(ApplicationDbContext context)
    {
        if (!await context.Example.AnyAsync())
        {
            await context.Example.AddRangeAsync(InitialData.ExampleWithItems);
            await context.SaveChangesAsync();
        }
    }
}
