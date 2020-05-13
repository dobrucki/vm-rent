using MediatR;

namespace RentService.Core.Application.QueryModel
{
    internal interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        
    }
}