using Mediator;

namespace BuildingBlocks.CQRS;

public interface IComandoHandler<in TCommand>
    : IComandoHandler<TCommand, Unit>
    where TCommand : IComando<Unit>
{
}

public interface IComandoHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : IComando<TResponse>
    where TResponse : notnull
{
}
