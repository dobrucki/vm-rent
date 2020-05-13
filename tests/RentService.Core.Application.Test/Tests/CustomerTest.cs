using System;
using System.Threading.Tasks;
using RentService.Core.Application.CommandModel.Customers;
using RentService.Core.Application.CommandModel.Customers.Commands;
using RentService.Core.Application.QueryModel.Customers;
using RentService.Core.Application.QueryModel.Customers.Queries;
using RentService.Core.Application.SharedKernel;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentService.Core.Application.Test.Mock;

namespace RentService.Core.Application.Test.Tests
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
        public async Task CreateCustomer_ValidCustomerProvided_CustomerCreated()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                EmailAddress = "mirek@mirek.pl",
                FirstName = "Wiktor",
                LastName = "Trzmiel"
            };
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

        [Test]
        public async Task EditCustomerDetails_DifferentDetails_FirstNameAndLastNameChanged()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),    
                EmailAddress = "mirek@mirek.pl",
                FirstName = "Wiktor",    
                LastName = "Trzmiel"
            };
            await _mediator.Send(new CreateCustomerCommand
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            });
            await _mediator.Send(new EditCustomerDetailsCommand
            {
                Id = customer.Id,
                FirstName = "Kasia",
                LastName = "Kudra"
            });
            var customerEntity = await _mediator.Send(new GetCustomerQuery
            {
                CustomerId = customer.Id
            });
            Assert.AreEqual("Kasia", customerEntity.FirstName);
            Assert.AreEqual("Kudra", customerEntity.LastName);
        }
    }
}