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
            try
            {
                var customer = await _customers.GetCustomerByIdAsync(request.CustomerId);
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.ModifiedAt = DateTime.UtcNow;
                await _customers.UpdateCustomerDetailsAsync(customer);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException("Could not edit customer.", new[]
                {
                    new PersistenceError(exception.ToString(), exception.Message)
                });
            }
            return Unit.Value;
        }
    }
}