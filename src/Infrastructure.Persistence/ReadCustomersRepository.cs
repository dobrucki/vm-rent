using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.QueryModel.Customers;
using Core.Application.SharedKernel.Events;
using Core.Application.SharedKernel.Exceptions;
using Infrastructure.Persistence.ReadOnlyEntities;
using MediatR;
using MongoDB.Driver;

namespace Infrastructure.Persistence
{
    public class ReadCustomersRepository : ICustomersQueryRepository,
        INotificationHandler<CustomerCreatedEvent>
    {

        private IMongoDatabase _database;

        public ReadCustomersRepository(IMongoClient client)
        {
            _database = client.GetDatabase("vm_rent");
        }
        public async Task<CustomerQueryEntity> GetCustomerByIdAsync(Guid customerId)
        {
            const string collectionName = "Customers";
            var collection = _database.GetCollection<CustomerReadOnlyEntity>(collectionName);
            var result = await (await collection
                    .FindAsync(x => x.Id == customerId))
                .FirstOrDefaultAsync();
            if (result is null) 
                throw new NotFoundException("Customer", customerId);
            return new CustomerQueryEntity
            {
                Id = result.Id.ToString(),
                EmailAddress = result.EmailAddress,
                FirstName = result.FirstName,
                LastName = result.LastName
            };
        }

        public async Task<IList<CustomerQueryEntity>> ListCustomersAsync(int limit, int offset)
        {
            const string collectionName = "Customers";
            var collection = _database.GetCollection<CustomerReadOnlyEntity>(collectionName);
            var result = await (await collection
                .FindAsync(Builders<CustomerReadOnlyEntity>.Filter.Empty))
                .ToListAsync();
            return result
                .Select(x => new CustomerQueryEntity
                {
                    Id = x.Id.ToString(),
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList();
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            const string collectionName = "Customers";
            var collection = _database.GetCollection<CustomerReadOnlyEntity>(collectionName);
            var customerEntity = new CustomerReadOnlyEntity
            {
                Id = notification.Customer.Id,
                EmailAddress = notification.Customer.EmailAddress,
                FirstName = notification.Customer.FirstName,
                LastName = notification.Customer.LastName
            };
            await collection.InsertOneAsync(customerEntity, cancellationToken);
        }
    }
}