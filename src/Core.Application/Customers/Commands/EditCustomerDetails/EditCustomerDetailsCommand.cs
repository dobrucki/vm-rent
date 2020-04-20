using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.Commands.EditCustomerDetails
{
    public class EditCustomerDetailsCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}