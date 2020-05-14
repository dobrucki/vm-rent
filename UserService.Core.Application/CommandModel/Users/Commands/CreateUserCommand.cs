using System;

namespace UserService.Core.Application.CommandModel.Users.Commands
{
    public sealed class CreateUserCommand : ICommand
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}