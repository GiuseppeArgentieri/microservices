using Mediator;

namespace BuildingBlocks.CQRS;
public interface IMediatorQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IMediatorQuery<TResponse>
    where TResponse : notnull
{
}
