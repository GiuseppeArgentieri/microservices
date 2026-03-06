using FluentValidation;
using Scheduler.Application.Dtos;

namespace Scheduler.Application.Events.Commands.ExampleEvent;

public record ExampleEventCommand(ExampleDto exampleDto)
    : IComando<ExampleEventResult>;

public record ExampleEventResult(Guid Id);

public class ExampleEventCommandValidator : AbstractValidator<ExampleEventCommand>
{
    public ExampleEventCommandValidator()
    {
        RuleFor(x => x.exampleDto.Id).NotNull().WithMessage("Id is required");
        RuleFor(x => x.exampleDto.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.exampleDto.OrderName).NotEmpty().WithMessage("OrderName should not be empty");
    }
}