using MediatR;

namespace Core.Application.SharedKernel
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
        
    }
}