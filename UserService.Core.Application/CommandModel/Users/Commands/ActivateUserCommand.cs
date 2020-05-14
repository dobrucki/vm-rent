using System;

namespace UserService.Core.Application.CommandModel.Users.Commands
{
    public class ActivateUserCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}