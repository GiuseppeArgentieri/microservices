namespace Scheduler.Application.Dtos;

public record CreateUserDto
{
    public string Email { get; init; } = default!;
    public string UserName { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string? DisplayName { get; init; }
    public string? Department { get; init; }
    public string? JobTitle { get; init; }
    public string? BusinessPhone { get; init; }
};

public record UpdateUserDto
{
    public string? DisplayName { get; init; }
    public string? Department { get; init; }
    public string? JobTitle { get; init; }
    public string? BusinessPhone { get; init; }
    public string? Email { get; init; }
}

public record UserResponseDto(
    Guid Id,
    string Email,
    string UserName,
    string? DisplayName,
    string? Department,
    string? JobTitle,
    string? BusinessPhone,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? LastLoginAt
);
