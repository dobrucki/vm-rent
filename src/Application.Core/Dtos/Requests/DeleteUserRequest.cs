using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class DeleteUserRequest :
        IRequest<BaseResponseDto<bool>>
    {
        public Guid Id { get; set; }
    }
}