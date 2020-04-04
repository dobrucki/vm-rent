using MediatR;

namespace Core.Application.Queries
{
    public class QueryBase<T> : IRequest<T> where T : class
    {
        
    }
}