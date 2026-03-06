using Mediator;

namespace BuildingBlocks.CQRS;
public interface IMediatorQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
