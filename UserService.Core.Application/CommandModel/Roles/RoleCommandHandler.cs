using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Core.Application.CommandModel.Roles.Commands;
using UserService.Core.Application.CommandModel.Roles.Events;
using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Roles
{
    public class RoleCommandHandler :
        ICommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _roles;
        private readonly IMediator _mediator;

        public RoleCommandHandler(IRoleRepository roles, IMediator mediator)
        {
            _roles = roles;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Id = request.Id,
                Name = request.Name
            };
            await _roles.InsertOneAsync(role);
            await _mediator.Publish(new RoleCreatedEvent
            {
                Role = role
            }, cancellationToken);
            return Unit.Value;
        }

    }
}