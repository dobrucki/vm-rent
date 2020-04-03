using MediatR;

namespace Core.Domain.Queries
{
    public class QueryBase<T> : IRequest<T> where T : class
    {
        
    }
}