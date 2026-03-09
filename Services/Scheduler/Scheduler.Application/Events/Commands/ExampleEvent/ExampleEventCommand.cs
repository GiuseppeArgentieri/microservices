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
        RuleFor(x => x.exampleDto.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.exampleDto.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.exampleDto.OrderName).NotEmpty().WithMessage("OrderName should not be empty");
    }
}