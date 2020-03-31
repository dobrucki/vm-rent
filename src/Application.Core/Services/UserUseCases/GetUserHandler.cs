using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.Core.Ports;
using Application.Core.Services.VirtualMachineUseCases;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Core.Services.UserUseCases
{
    public class GetUserHandler :
        IRequestHandler<GetUserRequest, BaseResponseDto<UserDto>>
    {
        private readonly ILogger<GetUserHandler> _logger;
        private readonly UserManager<User> _userManager;

        public GetUserHandler(
            ILogger<GetUserHandler> logger, 
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<BaseResponseDto<UserDto>> Handle(
            GetUserRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<UserDto>();

            try
            {

                var user = _userManager.Users.SingleOrDefault(u => u.Id.Equals(request.Id));
                    response.Data = new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username
                    };
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occured while getting virtual machine.");
            }

            return response;
        }
    }
}