using System;

namespace UserService.Application.Commands
{
    public class AddUserRoleCommand : ICommand
    {
        public Guid Id { get; }
        public string RoleName { get; }

        public AddUserRoleCommand(Guid id)
        {
            Id = id;
        }
    }
}