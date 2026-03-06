using BuildingBlocks.Pagination;
using Scheduler.Application.Events.Queries.GetExample;

namespace Scheduler.API.Endpoints;

//public class GetExample
//{
//}

public record GetExampleResponse(PaginatedResult<ExampleDto> Example);

public class GetExample : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/example", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetExampleQuery(request));

            var response = result.Adapt<GetExampleResponse>();

            return Results.Ok(response);
        })
        .WithName("GetExamples")
        .Produces<GetExampleResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Examples")
        .WithDescription("Get Examples");
    }
}