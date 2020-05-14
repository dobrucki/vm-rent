using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using UserService.Core.Application.CommandModel.Users.Commands;
using UserService.Core.Application.CommandModel.Users.Events;
using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users
{
    internal sealed class UserCommandHandler :
        ICommandHandler<CreateUserCommand>
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
                PasswordHash = HashPassword(request.Password)
            };
            await _users.InsertOneAsync(user);
            await _mediator.Publish(new UserCreatedEvent
            {
                User = user
            }, cancellationToken);
            return Unit.Value;
        }

        private static string HashPassword(string password)
        {
            var hasher = SHA384.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var t in hash)
            {
                builder.Append(t.ToString("x2"));
            }
    
            return builder.ToString();
        }
    }
}