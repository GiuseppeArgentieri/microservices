using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduler.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.DisplayName).IsRequired().HasMaxLength(500);
        builder.Property(u => u.Department).IsRequired().HasMaxLength(500);
        builder.Property(u => u.JobTitle).IsRequired().HasMaxLength(500);
        builder.Property(u => u.businessPhone).IsRequired().HasMaxLength(500);
    }
}