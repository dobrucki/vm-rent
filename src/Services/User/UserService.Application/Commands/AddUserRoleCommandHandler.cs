using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Commands
{
    public class AddUserRoleCommandHandler : ICommandHandler<AddUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            switch (request.RoleName)
            {
                case "Administrator":
                    user.AddRole(UserRole.Administrator);
                    break;
                case "Manager":
                    user.AddRole(UserRole.Manager);
                    break;
                case "Client":
                    user.AddRole(UserRole.Client);
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