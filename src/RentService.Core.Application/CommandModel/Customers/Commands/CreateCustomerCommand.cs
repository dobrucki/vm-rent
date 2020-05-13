using System;
using RentService.Core.Application.SharedKernel;

namespace RentService.Core.Application.CommandModel.Customers.Commands
{
    public sealed class CreateCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}