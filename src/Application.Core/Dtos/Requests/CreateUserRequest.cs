using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class CreateUserRequest :
        IRequest<BaseResponseDto<UserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}