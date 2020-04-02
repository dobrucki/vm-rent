using MediatR;

namespace Application.Domain.Events
{
    public class EventBase<T> : IRequest<T> where T : class
    {
        
    }
}