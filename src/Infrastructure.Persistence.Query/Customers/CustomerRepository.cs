using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.CommandModel;
using Core.Application.CommandModel.Customers.Events;
using Core.Application.QueryModel.Customers;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Query.Customers
{
    public class CustomerRepository : ICustomersQueryRepository,
        IEventHandler<CustomerCreatedEvent>,
        IEventHandler<CustomerDetailsEditedEvent>
    {
        private readonly IMongoCollection<CustomerEntity> _customers;
        private readonly IMapper _mapper;

        public CustomerRepository(IMongoClient client, IMapper mapper)
        {
            _mapper = mapper;
            var database = client.GetDatabase("vm_rent");
            _customers = database.GetCollection<CustomerEntity>("Customers");
        }
        
        public async Task<CustomerQueryEntity> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _customers
                .Find(x => x.Id.Equals(customerId.ToString()))
                .SingleOrDefaultAsync();
            return new CustomerQueryEntity
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        public async Task<IList<CustomerQueryEntity>> ListCustomersAsync(int limit, int offset)
        {
            var customers = await _customers
                .Find(_ => true)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return customers.Select(x => new CustomerQueryEntity
            {
                Id = x.Id,
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerEntity = new CustomerEntity
            {
                Id = notification.Customer.Id.ToString(),
                EmailAddress = notification.Customer.EmailAddress,
                FirstName = notification.Customer.FirstName,
                LastName = notification.Customer.LastName
            };
            await _customers.InsertOneAsync(customerEntity, cancellationToken);
        }

        public async Task Handle(CustomerDetailsEditedEvent notification, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerEntity>.Filter
                .Eq(x => x.Id, notification.Customer.Id.ToString());

            var update = Builders<CustomerEntity>.Update
                .Set(x => x.FirstName, notification.Customer.FirstName)
                .Set(x => x.LastName, notification.Customer.LastName);

            await _customers.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}