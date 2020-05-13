using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RentService.Core.Application.QueryModel.Customers.Queries;
using RentService.Core.Application.SharedKernel.Exceptions;

namespace RentService.Core.Application.QueryModel.Customers
{
    internal sealed class CustomerQueryHandler : 
        IQueryHandler<GetCustomerQuery, CustomerQueryEntity>,
        IQueryHandler<ListCustomersQuery, IList<CustomerQueryEntity>>
    {
        private readonly ICustomersQueryRepository _customers;

        public CustomerQueryHandler(ICustomersQueryRepository customers)
        {
            _customers = customers;
        }


        public async Task<CustomerQueryEntity> Handle(
            GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customers.GetCustomerByIdAsync(request.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException("Customer", request.CustomerId);
            }
            return customer;
        }

        public async Task<IList<CustomerQueryEntity>> Handle(
            ListCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customers.ListCustomersAsync(request.Limit, request.Offset) 
                   ?? new List<CustomerQueryEntity>(0);
        }
    }
}