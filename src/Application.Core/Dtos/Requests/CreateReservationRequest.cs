using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class CreateReservationRequest :
        IRequest<BaseResponseDto<ReservationDto>>
    {
        public Guid UserId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}