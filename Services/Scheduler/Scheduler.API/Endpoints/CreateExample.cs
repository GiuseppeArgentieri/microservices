using Scheduler.Application.Events.Commands.ExampleEvent;

namespace Scheduler.API.Endpoints;

//- Accepts a CreateExampleRequest object.
//- Maps the request to a ExampleEventCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created example's ID.

public record CreateExampleRequest(ExampleDto ExampleDto);
public record CreateExampleResponse(Guid Id);

public class CreateExample : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/example", async (CreateExampleRequest request, IMediator sender) =>
        {
            var command = request.Adapt<ExampleEventCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateExampleResponse>();

            return Results.Created($"/example/{response.Id}", response);
        })
        .WithName("CreateExample")
        .Produces<CreateExampleResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Example")
        .WithDescription("Create Example");
    }
}