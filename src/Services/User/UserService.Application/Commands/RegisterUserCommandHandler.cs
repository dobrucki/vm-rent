using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Commands
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = Hashing.HashPassword(request.Password);
            var user = new User(request.Id, request.FirstName, request.LastName, request.EmailAddress, passwordHash);
            await _userRepository.InsertAsync(user);
            var result = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result) throw new Exception();
            return Unit.Value;
        }
        
        
    }
}