using System;
using Core.Application.SharedKernel;

namespace Core.Application.CommandModel.Customers.Commands
{
    public sealed class CreateCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}