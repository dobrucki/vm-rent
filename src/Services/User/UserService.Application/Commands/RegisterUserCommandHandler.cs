using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Helpers;
using UserService.Application.IntegrationEvents;
using UserService.Application.IntegrationEvents.Events;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Commands
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public RegisterUserCommandHandler(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = Hashing.HashPassword(request.Password);
            var user = new User(request.Id, request.Login, request.FirstName, 
                request.LastName, request.EmailAddress, passwordHash);
            await _userRepository.InsertAsync(user);
            await _userRepository.UnitOfWork.CommitAsync(cancellationToken);
            _eventBus.Publish(UserCreatedIntegrationEvent.FromUser(user));
            return Unit.Value;
        }
    }
}