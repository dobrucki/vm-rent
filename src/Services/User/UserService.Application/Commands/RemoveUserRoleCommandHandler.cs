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
                    user.RemoveRole(UserRole.Administrator);
                    break;
                case "Manager":
                    user.RemoveRole(UserRole.Manager);
                    break;
                case "Client":
                    user.RemoveRole(UserRole.Client);
                    break;
                default:
                    throw new Exception();
            }

            var result = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result) throw new Exception();
            return Unit.Value;
        }
    }
}