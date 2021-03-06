using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Commands
{
    public class RemoveUserRoleCommandHandler : ICommandHandler<RemoveUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveUserRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            switch (request.RoleName)
            {
                case "Administrator":
                    user.RemoveRole(Role.Administrator);
                    break;
                case "Manager":
                    user.RemoveRole(Role.Manager);
                    break;
                case "Client":
                    user.RemoveRole(Role.Client);
                    break;
                default:
                    throw new Exception();
            }

            await _userRepository.UnitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}