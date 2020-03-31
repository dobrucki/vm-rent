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
    public class DeleteUserHandler
        : IRequestHandler<DeleteUserRequest, BaseResponseDto<bool>>
    {
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly UserManager<User> _userManager;

        public DeleteUserHandler(
            ILogger<DeleteUserHandler> logger, 
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        
        public async Task<BaseResponseDto<bool>> Handle(
            DeleteUserRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Id.Equals(request.Id));
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Deleted user with id {request.Id}");
                }
                else
                {
                    response.Errors
                        .AddRange(result.Errors.AsEnumerable()
                            .Select(e => $"{e.Description}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}