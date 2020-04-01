using System.Collections.Generic;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class ListAllReservationsRequest :
        IRequest<BaseResponseDto<List<ReservationDto>>>
    {
        
    }
}