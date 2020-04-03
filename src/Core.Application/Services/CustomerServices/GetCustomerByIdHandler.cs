using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.Queries.CustomerQueries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services.CustomerServices
{
    public class GetCustomerByIdHandler :
        IRequestHandler<GetCustomerByIdQuery, BaseResponseDto<CustomerDto>>
    {
        
        private readonly ILogger<GetCustomerByIdHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdHandler(
            ILogger<GetCustomerByIdHandler> logger, 
            IMediator mediator, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<CustomerDto>> Handle(
            GetCustomerByIdQuery request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<CustomerDto>();
            try
            {
                Customer customer;
                using (_unitOfWork)
                {
                    customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
                }

                if (customer == null)
                {
                    response.Errors.Add($"Customer with id {request.CustomerId} does not exist.");
                }

                if (response.HasError || customer == null)
                { 
                    throw new ApplicationException("Some errors occurred while getting customer");
                }

                response.Data = new CustomerDto 
                {
                    Id = customer.Id,
                    EmailAddress = customer.EmailAddress,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
//                response.Errors.Add(ex.Message);
            }
            
            return response;
        }
        
    }
}