using MediatR;

namespace Application.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        
    }
}