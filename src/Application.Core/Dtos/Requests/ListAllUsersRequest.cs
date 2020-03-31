using System.Collections.Generic;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class ListAllUsersRequest :
        IRequest<BaseResponseDto<List<UserDto>>>
    {
        
    }
}