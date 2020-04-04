using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dtos;
using Core.Application.Interfaces;
using Core.Domain.Commands.RentalCommands;
using Core.Domain.Events.RentalEvents;
using Core.Domain.Models;
using Core.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using CreateRentalCommand = Core.Application.Commands.RentalCommands.CreateRentalCommand;
using RentalCreatedEvent = Core.Application.Events.RentalEvents.RentalCreatedEvent;

namespace Core.Application.Services.RentalServices
{
    public class CreateRentalHandler :
        IRequestHandler<CreateRentalCommand, Result<RentalDto>>
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
        
        public async Task<Result<RentalDto>> Handle(
            CreateRentalCommand request, CancellationToken cancellationToken = default)
        {
            var response = new Result<RentalDto>();
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