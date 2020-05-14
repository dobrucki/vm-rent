using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserService.Core.Application.CommandModel;
using UserService.Core.Application.CommandModel.Roles.Events;
using UserService.Core.Application.QueryModel.Roles;

namespace UserService.Infrastructure.Persistence.Query.Roles
{
    public class RoleRepository : IRolesQueryRepository,
        IEventHandler<RoleCreatedEvent>
    {
        private readonly IMongoCollection<RoleEntity> _roles;

        public RoleRepository(IMongoClient client)
        {
            var database = client.GetDatabase("vm_rent");
            _roles = database.GetCollection<RoleEntity>("Roles");
        }
        public async Task<RoleQueryEntity> GetRoleByIdAsync(Guid roleId)
        {
            var role = await _roles
                .Find(x => x.Id.Equals(roleId.ToString()))
                .SingleOrDefaultAsync();
            return new RoleQueryEntity
            {
                Id = role.Id,
                Name = role.Name
            };    
        }

        public async Task<IList<RoleQueryEntity>> ListRolesAsync(int limit, int offset)
        {
            var roles = await _roles
                .Find(_ => true)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return roles.Select(x => new RoleQueryEntity
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task Handle(RoleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerEntity = new RoleEntity
            {
                Id = notification.Role.Id.ToString(),
                Name = notification.Role.Name,
            };
            await _roles.InsertOneAsync(customerEntity, cancellationToken);
        }
    }
}