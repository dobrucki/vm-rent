using MediatR;

namespace Core.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        
    }
}