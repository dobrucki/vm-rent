using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Commands.Rental;
using Application.Domain.Dtos;
using Application.Domain.Events.Customer;
using Application.Domain.Events.Rental;
using Application.Domain.Models;
using Application.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Service.Services.RentalServices
{
    public class CreateRentalHandler :
        IRequestHandler<CreateRentalCommand, BaseResponseDto<RentalDto>>
    {
        private readonly ILogger<CreateRentalHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRentalHandler(
            ILogger<CreateRentalHandler> logger, 
            IUnitOfWork unitOfWork, 
            IMediator mediator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        
        public async Task<BaseResponseDto<RentalDto>> Handle(
            CreateRentalCommand request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<RentalDto>();
            try
            {
                if (request.StartTime < DateTime.UtcNow)
                {
                    response.Errors
                        .Add($"Start time can not be from the past.");
                }

                if (request.EndTime < DateTime.UtcNow)
                {
                    response.Errors
                        .Add($"End time can not be from the past.");
                }

                if (request.StartTime > request.EndTime)
                {
                    response.Errors
                        .Add($"Start time can not be later than end time.");
                }
                
                Rental rental;
                using (_unitOfWork)
                {
                    var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
                    if (customer == null)
                    {
                        response.Errors
                            .Add($"Customer with id {request.CustomerId} does not exist.");
                    }
                    var virtualMachine = await _unitOfWork.VirtualMachines.GetByIdAsync(request.VirtualMachineId);
                    if (virtualMachine == null)
                    {
                        response.Errors
                            .Add($"VirtualMachine with id {request.VirtualMachineId} does not exist.");

                    }
                    if (response.HasError || customer == null || virtualMachine == null)
                    {
                        throw new ApplicationException(
                            "Could not create rental because some errors occurred.");
                    }
                    rental = new Rental
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = null,

                        Customer = customer,
                        VirtualMachine = virtualMachine,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime
                    };
                    await _unitOfWork.Rentals.InsertAsync(rental);
                    _unitOfWork.Complete();
                }

                await _mediator.Publish(new RentalCreatedEvent
                {
                    RentalId = rental.Id
                }, cancellationToken);
                
                response.Data = new RentalDto
                {
                    Id = rental.Id,
                    CustomerId = rental.Customer.Id,
                    VirtualMachineId = rental.VirtualMachine.Id,
                    StartTime = rental.StartTime,
                    EndTime = rental.EndTime
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //response.Errors.Add(ex.Message);
            }
            
            return response;
        }
    }
}