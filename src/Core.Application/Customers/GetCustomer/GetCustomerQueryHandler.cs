using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerQueryHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }


        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
                if (customer is null)
                {
                    return null;
                }

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
}