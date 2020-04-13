using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.EditCustomerDetails
{
    public class EditCustomerDetailsCommandHandler : IRequestHandler<EditCustomerDetailsCommand>
    {
        private readonly ICustomersRepository _customers;
        private readonly ILogger<EditCustomerDetailsCommandHandler> _logger;

        public EditCustomerDetailsCommandHandler( 
            ILogger<EditCustomerDetailsCommandHandler> logger, 
            ICustomersRepository customers)
        {
            _logger = logger;
            _customers = customers;
        }

        public async Task<Unit> Handle(EditCustomerDetailsCommand request, CancellationToken cancellationToken)
        {
            
            var customer = await _customers.GetCustomerByIdAsync(request.CustomerId);
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            await _customers.UpdateCustomerDetailsAsync(customer);
            _logger.LogInformation($"Updated customer ({customer.Id})");
            
            return Unit.Value;
        }
    }
}