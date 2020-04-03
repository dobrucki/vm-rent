using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.Models.Entities;
using Core.Domain.Queries.RentalQueries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services.RentalServices
{
    public class GetRentalByIdHandler :
        IRequestHandler<GetRentalByIdQuery, BaseResponseDto<RentalDto>>
    {
        private readonly ILogger<GetRentalByIdHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetRentalByIdHandler(
            ILogger<GetRentalByIdHandler> logger, 
            IMediator mediator, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<RentalDto>> Handle(
            GetRentalByIdQuery request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<RentalDto>();
            try
            {
                Rental rental;
                using (_unitOfWork)
                {
                    rental = await _unitOfWork.Rentals.GetByIdAsync(request.Id);
                }

                if (rental == null)
                {
                    response.Errors.Add($"Rental with id {request.Id} does not exist.");
                }

                if (response.HasError || rental == null)
                { 
                    throw new ApplicationException("Some errors occurred while getting rental");
                }

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
//                response.Errors.Add(ex.Message);
            }
            
            return response;
        }
    }
}