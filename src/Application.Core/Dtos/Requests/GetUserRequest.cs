using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class GetUserRequest :
        IRequest<BaseResponseDto<UserDto>>
    {
        public Guid Id { get; set; }
    }
}