using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Core.Services.UserUseCases
{
    public class ListAllUsersHandler : 
        IRequestHandler<ListAllUsersRequest, BaseResponseDto<List<UserDto>>>
    {
        private UserManager<User> _userManager;
        private ILogger<ListAllUsersHandler> _logger;

        public ListAllUsersHandler(
            UserManager<User> userManager, 
            ILogger<ListAllUsersHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<BaseResponseDto<List<UserDto>>> Handle(
            ListAllUsersRequest request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<List<UserDto>>();
            try
            {
                var users = _userManager.Users
                    .Select(e => new UserDto
                    {
                        Id = e.Id,
                        Username = e.Username
                    })
                    .ToList();
                response.Data = users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occured while getting users.");
            }

            return response;
        }
    }
}