using MediatR;

namespace Core.Application.CommandModel
{
    internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
        
    }
}