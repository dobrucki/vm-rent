using MediatR;

namespace UserService.Api.Application.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T, Unit> where T : ICommand
    { }
}