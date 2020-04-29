using System;
using System.Threading.Tasks;
using Core.Application.CommandModel.Customers;
using Core.Application.CommandModel.Customers.Commands;
using Core.Application.QueryModel.Customers;
using Core.Application.QueryModel.Customers.Queries;
using Core.Application.SharedKernel;
using Core.Application.Test.Mock;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NUnit.Framework;

namespace Core.Application.Test.Tests
{
    [TestFixture]
    public class CustomerTest
    {

        private readonly IMediator _mediator;
        
        public CustomerTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            
            var customerRepository = new MockCustomerRepository();
            services.AddSingleton<ICustomerRepository>(customerRepository);
            services.AddSingleton<ICustomersQueryRepository>(customerRepository);

            services.AddApplicationServices();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }
        
        [SetUp]
        public void SetUp()
        {
        }
        
        [Test]
        public async Task CreateCustomerTest()
        {
            var customer = new Customer
            {
                Id = Guid.Parse("5772c3c7-f2a0-468e-a054-6a75aba262e2"),
                EmailAddress = "mirek@mirek.pl",
                FirstName = "Wiktor",
                LastName = "Trzmiel"
            };
            Console.WriteLine($"{customer.Id}");
            await _mediator.Send(new CreateCustomerCommand
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            });
            var customerEntity = await _mediator.Send(new GetCustomerQuery
            {
                CustomerId = customer.Id
            });
            Assert.AreEqual(customer.Id.ToString(), customerEntity.Id.ToString());
            Assert.AreEqual(customer.EmailAddress, customerEntity.EmailAddress);
            Assert.AreEqual(customer.FirstName, customerEntity.FirstName);
            Assert.AreEqual(customer.LastName, customerEntity.LastName);
        }
    }
}