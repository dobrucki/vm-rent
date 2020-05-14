using System;

namespace UserService.Core.Application.CommandModel.Users.Commands
{
    public class DeactivateUserCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}