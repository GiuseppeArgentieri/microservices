using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Data;
using Scheduler.Application.Dtos;
using Scheduler.Application.Extensions;

namespace Scheduler.Application.Events.Queries.GetExample
{
    public class GetExampleHandler(IApplicationDbContext dbContext)
    : IMediatorQueryHandler<GetExampleQuery, GetExampleResult>
    {
        public async ValueTask<GetExampleResult> Handle(GetExampleQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Example.LongCountAsync(cancellationToken);

            var orders = await dbContext.Example
                           .Include(o => o.ExampleItems)
                           .OrderBy(o => o.CreatedAt)
                           .Skip(pageSize * pageIndex)
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

            return new GetExampleResult(
                new PaginatedResult<ExampleDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToExampleDtoList()));
        }
    }
}
