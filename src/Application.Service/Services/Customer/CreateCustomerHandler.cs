using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Events.Customer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Service.Services
{
    using Domain.Models;
    using Domain.Dtos;
    using Domain.Commands.Customer;
    using Interfaces;
    
    public class CreateCustomerHandler : 
        IRequestHandler<CreateCustomerCommand, BaseResponseDto<CustomerDto>>
    {
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(
            ILogger<CreateCustomerHandler> logger, 
            IUnitOfWork unitOfWork, 
            IMediator mediator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<CustomerDto>> Handle(
            CreateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<CustomerDto>();
            try
            {
                Expression<Func<Customer, bool>> predicate = x =>
                    x.EmailAddress.ToLower().Equals(request.EmailAddress.ToLower());

                Customer customer;
                using (_unitOfWork)
                {
                    if ((await _unitOfWork.Customers.GetAllAsync(predicate)).Any())
                    {
                        throw new ArgumentException($"Email {request.EmailAddress} already exists.");
                    }
                    customer = new Customer
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = null,

                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        EmailAddress = request.EmailAddress
                    };
                    await _unitOfWork.Customers.InsertAsync(customer);
                    _unitOfWork.Complete();
                }

                await _mediator.Publish(new CustomerCreatedEvent
                {
                    CustomerId = customer.Id
                }, cancellationToken);
                
                response.Data = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    EmailAddress = customer.EmailAddress
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add(ex.Message);
            }
            
            return response;
        }
    }
}