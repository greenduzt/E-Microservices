using MediatR;

namespace BuildingBlocks.CQRS;

/* 
 * The interface ICommandHandler<in TCommand> is an example of using generics to define a handler for commands that do not return a result. 
 * This interface extends another interface ICommandHandler<TCommand, Unit>, where Unit is used to signify a void return type
 */
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand<Unit>
{

}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
                                                                                                where TResponse : notnull
{
}

