using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;

namespace Scheduler.Infrastructure.Data.Configurations;

public class ExampleExtensionConfiguration: IEntityTypeConfiguration<ExampleExtension>
{
    public void Configure(EntityTypeBuilder<ExampleExtension> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => ExampleExtensionId.Of(value));

        builder.Property(x => x.ExampleId)
            .HasConversion(
                id => id.Value,
                value => ExampleId.Of(value))
            .IsRequired();

        builder.HasOne<Example>()
               .WithMany(e => e.ExampleItems)
               .HasForeignKey(x => x.ExampleId);

        builder.OwnsOne(x => x.ExampleName, name =>
        {
            name.Property(p => p.Value)
                .HasColumnName("ExampleName")
                .IsRequired();
        });

        builder.Property(x => x.Quantity)
               .IsRequired();
    }
}
