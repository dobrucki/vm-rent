using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class DeleteReservationRequest :
        IRequest<BaseResponseDto<bool>>
    {
        
    }
}