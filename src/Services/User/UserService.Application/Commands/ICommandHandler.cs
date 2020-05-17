using MediatR;

namespace UserService.Application.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T, Unit> where T : ICommand
    { }
}