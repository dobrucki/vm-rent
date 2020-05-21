using MediatR;

namespace RentingService.Application.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T, Unit> where T : ICommand
    { }
}