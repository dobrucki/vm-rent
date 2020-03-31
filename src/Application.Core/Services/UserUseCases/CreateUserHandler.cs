using System;
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
    public class CreateUserHandler :
        IRequestHandler<CreateUserRequest, BaseResponseDto<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(
            ILogger<CreateUserHandler> logger,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<BaseResponseDto<UserDto>> Handle(
            CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<UserDto>();

            try
            {

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username
                };
                await _userManager.CreateAsync(user, request.Password);
                response.Data = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username
                };
                _logger.LogInformation($"Created user with id {user.Id}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the virtual machine.");
            }

            return response;
        }
    }
}