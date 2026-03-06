using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    [MaxLength(500)]
    public string? DisplayName { get; set; }

    [MaxLength(500)]
    public string? Department { get; set; }

    [MaxLength(500)]
    public string? JobTitle { get; set; }

    [MaxLength(50)]
    public string? businessPhone { get; set; }
}
