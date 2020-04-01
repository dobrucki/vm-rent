using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.Core.Ports;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Core.Services.ReservationUseCases
{
    public class CreateReservationHandler :
        IRequestHandler<CreateReservationRequest, BaseResponseDto<ReservationDto>>
    {
        private readonly ILogger<CreateReservationHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CreateReservationHandler(
            ILogger<CreateReservationHandler> logger, 
            IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<BaseResponseDto<ReservationDto>> Handle(CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<ReservationDto>();

            try
            {
                var user = _userManager.Users.SingleOrDefault(e => e.Id.Equals(request.UserId));
                var virtualMachine = await _unitOfWork.VirtualMachines.GetByIdAsync(request.VirtualMachineId);
                var reservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    VirtualMachine = virtualMachine,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime
                };
                await _unitOfWork.Reservations.InsertAsync(reservation);
                _unitOfWork.Complete();
                response.Data = new ReservationDto
                {
                    Id = reservation.Id,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username
                    },
                    VirtualMachine = new VirtualMachineDto
                    {
                        Id = virtualMachine.Id,
                        Name = virtualMachine.Name
                    },
                    StartTime = request.StartTime,
                    EndTime = request.EndTime
                };
                _logger.LogInformation($"Created reservation with id {reservation.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the reservation.");
            }

            return response;
        }
    }
}