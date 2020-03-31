using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.Core.Ports;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Core.Services.UserUseCases
{
    public class UpdateUserHandler :
        IRequestHandler<UpdateUserRequest, BaseResponseDto<bool>>
    {
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly UserManager<User> _userManager;

        public UpdateUserHandler(
            ILogger<UpdateUserHandler> logger, 
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<BaseResponseDto<bool>> Handle(
            UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Id.Equals(request.Id));
                if (user == null) throw new ArgumentException("That user does not exist");
                user.Username = request.Username;
                var result = await _userManager
                    .ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Updated user with id {request.Id}");
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