using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class UpdateUserRequest :
        IRequest<BaseResponseDto<bool>>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}