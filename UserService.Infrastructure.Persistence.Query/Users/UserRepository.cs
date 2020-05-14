using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using UserService.Core.Application.CommandModel;
using UserService.Core.Application.CommandModel.Users.Events;
using UserService.Core.Application.QueryModel.Users;

namespace UserService.Infrastructure.Persistence.Query.Users
{
    public class UserRepository : IUsersQueryRepository,
        IEventHandler<UserCreatedEvent>
    {
        private readonly IMongoCollection<UserEntity> _customers;
        private readonly IMapper _mapper;

        public UserRepository(IMongoClient client, IMapper mapper)
        {
            _mapper = mapper;
            var database = client.GetDatabase("vm_rent");
            _customers = database.GetCollection<UserEntity>("Users");
        }
        
        public async Task<UserQueryEntity> GetUserByIdAsync(Guid customerId)
        {
            var customer = await _customers
                .Find(x => x.Id.Equals(customerId.ToString()))
                .SingleOrDefaultAsync();
            return new UserQueryEntity
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        public async Task<IList<UserQueryEntity>> ListUsersAsync(int limit, int offset)
        {
            var customers = await _customers
                .Find(_ => true)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return customers.Select(x => new UserQueryEntity
            {
                Id = x.Id,
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerEntity = new UserEntity
            {
                Id = notification.User.Id.ToString(),
                EmailAddress = notification.User.EmailAddress
            };
            await _customers.InsertOneAsync(customerEntity, cancellationToken);
        }
    }
}