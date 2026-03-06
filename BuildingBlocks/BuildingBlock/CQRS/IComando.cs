using Mediator;

namespace BuildingBlocks.CQRS;

public interface IComando : IComando<Unit>
{
}

public interface IComando<out TResponse> : IRequest<TResponse>
{
}
