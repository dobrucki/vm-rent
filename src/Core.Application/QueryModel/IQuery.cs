using MediatR;

namespace Core.Application.QueryModel
{
    internal interface IQuery<out TResult> : IRequest<TResult>
    {
        
    }
}