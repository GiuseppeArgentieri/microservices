using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Scheduler.Application.Dtos;

namespace Scheduler.Application.Events.Queries.GetExample;

public record GetExampleQuery(PaginationRequest PaginationRequest)
    : IMediatorQuery<GetExampleResult>;

public record GetExampleResult(PaginatedResult<ExampleDto> Example);