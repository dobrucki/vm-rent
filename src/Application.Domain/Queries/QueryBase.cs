using MediatR;

namespace Application.Domain.Queries
{
    public class QueryBase<T> : IRequest<T> where T : class
    {
        
    }
}