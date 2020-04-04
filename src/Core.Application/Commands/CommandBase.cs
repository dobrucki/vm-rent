using MediatR;

namespace Core.Application.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        
    }
}