using MediatR;

namespace BuildingBlocks.CQRS
{
    /*
     * Unit is a type used to represent a void return type in MediatR. It is similar to void in method signatures 
     * but can be used in generic type parameters. Using Unit allows commands to adhere to the MediatR pattern even 
     * if they don't need to return any data.
     * 
     * The purpose of defining ICommand this way is to create a simpler interface for commands that do not need to 
     * return any result. This makes it easier to work with commands that perform actions but do not produce a result.
     */
    public interface ICommand : ICommand<Unit>
    {

    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
