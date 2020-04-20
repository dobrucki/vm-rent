using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}