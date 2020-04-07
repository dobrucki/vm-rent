using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateCustomerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null,

                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress
            };
            try
            {
                using (_unitOfWork)
                {
                    await _unitOfWork.Customers.InsertAsync(customer);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException(
                    "Could not create customer.", 
                    new []{new PersistenceError(exception.ToString(), exception.Message)});
            }

            _logger.LogInformation($"Created customer with id {customer.Id}.");
            return new CustomerDto
            {
                CustomerId = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }
    }
}