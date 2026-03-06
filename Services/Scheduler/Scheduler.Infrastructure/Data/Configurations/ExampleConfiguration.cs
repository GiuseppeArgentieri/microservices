using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Scheduler.Infrastructure.Data.Configurations;

public class ExampleConfiguration : IEntityTypeConfiguration<Example>
{
    public void Configure(EntityTypeBuilder<Example> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CustomerId).IsRequired();
        builder.Property(e => e.OrderName).IsRequired().HasMaxLength(100);

        builder.Property(e => e.Id)
        .HasConversion(
            id => id.Value,        // da ExampleId a Guid
            value => ExampleId.Of(value)) // da Guid a ExampleId
        .IsRequired();

        builder.HasMany(e => e.ExampleItems)
            .WithOne()
            .HasForeignKey(ei => ei.ExampleId);
    }

}

