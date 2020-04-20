using MediatR;

namespace Core.Application.SharedKernel
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        
    }
}