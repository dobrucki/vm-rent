using MediatR;

namespace RentService.Core.Application.QueryModel
{
    internal interface IQuery<out TResult> : IRequest<TResult>
    {
        
    }
}