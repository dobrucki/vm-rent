using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCustomerQueryHandler> _logger;

        public GetCustomerQueryHandler(IMediator mediator, IUnitOfWork unitOfWork, ILogger<GetCustomerQueryHandler> logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Handling request....");
            using (_unitOfWork)
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
                if (customer is null)
                {
                    return null;
                }

                _logger.LogDebug("Request handled.");
                var customerDto = new CustomerDto
                {
                    CustomerId = customer.Id,
                    EmailAddress = customer.EmailAddress,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };
                return customerDto;
            }
        }
    }
}