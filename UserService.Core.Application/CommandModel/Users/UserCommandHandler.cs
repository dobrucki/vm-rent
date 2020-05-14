using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using UserService.Core.Application.CommandModel.Users.Commands;
using UserService.Core.Application.CommandModel.Users.Events;
using UserService.Core.Application.SharedKernel;
using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users
{
    internal sealed class UserCommandHandler :
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<ActivateUserCommand>,
        ICommandHandler<DeactivateUserCommand>
    {
        private readonly IUserRepository _users;
        private readonly IMediator _mediator;

        public UserCommandHandler(IUserRepository users, IMediator mediator)
        {
            _users = users;
            _mediator = mediator;
        }
        
        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                EmailAddress = request.EmailAddress,
                PasswordHash = Hashing.HashPassword(request.Password),
                IsActive = false
            };
            await _users.InsertOneAsync(user);
            await _mediator.Publish(new UserCreatedEvent
            {
                User = user
            }, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByIdAsync(request.UserId);
            user.IsActive = true;
            await _users.UpdateOneAsync(user);
            var userActivatedEvent = new UserActivatedEvent
            {
                User = user
            };
            await _mediator.Publish(userActivatedEvent, cancellationToken);
            return Unit.Value;
        }
        
        public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByIdAsync(request.UserId);
            user.IsActive = false;
            await _users.UpdateOneAsync(user);
            var userActivatedEvent = new UserActivatedEvent
            {
                User = user
            };
            await _mediator.Publish(userActivatedEvent, cancellationToken);
            return Unit.Value;
        }
    }
}