using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.CommandModel.Customers;
using Core.Application.QueryModel.Customers;
using Core.Application.SharedKernel.Exceptions;
using Core.Domain.Customers;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Core.Application.Test.Mock
{
    public class MockCustomerRepository : 
        ICustomerRepository,
        ICustomersQueryRepository
    {
        private readonly IList<MockCustomerEntity> _customers;

        public MockCustomerRepository()
        {
            _customers = new List<MockCustomerEntity>();
        }

        public Task<Customer> GetByIdAsync(Guid id)
        {
            var customerEntity = _customers.SingleOrDefault(x => x.Id.Equals(id));
            if (customerEntity == null) return null;
            var customer = new Customer
            {
                Id = customerEntity.Id,
                EmailAddress = customerEntity.EmailAddress,
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName
            };
            return Task.FromResult(customer);
        }

        public Task InsertOneAsync(Customer customer)
        {
            var customerEntity = new MockCustomerEntity
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress
            };
            _customers.Add(customerEntity);
            return Task.CompletedTask;
        }

        public Task UpdateOneAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerQueryEntity> GetCustomerByIdAsync(Guid customerId)
        {
            var customerEntity = _customers.SingleOrDefault(x => x.Id.Equals(customerId));
            if (customerEntity is null) throw new NotFoundException("Customer", customerId);
            return Task.FromResult(new CustomerQueryEntity
            {
                    Id = customerEntity.Id.ToString(),
                    FirstName = customerEntity.FirstName,
                    LastName = customerEntity.LastName,
                    EmailAddress = customerEntity.EmailAddress
            });
        }

        public Task<IList<CustomerQueryEntity>> ListCustomersAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }
    }
}