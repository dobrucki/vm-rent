using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Commands
{
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public ActivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            user.ActivateUser();
            await _userRepository.UnitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}