namespace Scheduler.Application.Dtos;

public record ExampleDto(
    Guid Id,
    Guid CustomerId,
    string OrderName
    );
