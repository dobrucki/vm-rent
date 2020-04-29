using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.CommandModel.Customers.Commands;
using Core.Application.QueryModel.Customers.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using UserInterface.SoapApi.Models;

namespace UserInterface.SoapApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IMediator mediator, ILogger<CustomerService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public CustomerModel GetCustomer(string id)
        {
            var query = new GetCustomerQuery
            {
                CustomerId = Guid.Parse(id)
            };
            var customer = _mediator.Send(query).Result;
            return new CustomerModel
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }
        
        public CustomerListModel ListCustomers(int offset, int limit)
        {
            var query = new ListCustomersQuery()
            {
                Offset = offset,
                Limit = limit
            };
            var customers = _mediator.Send(query).Result;
            var customersList = new CustomerListModel
            {
                Customers = customers.Select(customer =>
                    new CustomerModel
                    {
                        Id = customer.Id,
                        EmailAddress = customer.EmailAddress,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName
                    }).ToList()
            };
            return customersList;
        }

        public void EditCustomerDetails(string id, string firstName, string lastName)
        {
            var command = new CreateCustomerCommand
            {
                Id = Guid.Parse(id),
                FirstName = firstName,
                LastName = lastName
            };
            Task.Run(async () => await _mediator.Send(command));
        }

        public void CreateCustomer(CustomerModel customer)
        {
            var command = new CreateCustomerCommand
            {
                Id = Guid.Parse(customer.Id),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress
            };
            _logger.LogError(command.Id.ToString());
            _mediator.Send(command).Wait();
        }
    }
}