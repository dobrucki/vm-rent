using System;

namespace UserService.Application.Commands
{
    public class RemoveUserRoleCommand : ICommand
    {
        public Guid Id { get; }
        public string RoleName { get; }

        public RemoveUserRoleCommand(Guid id)
        {
            Id = id;
        }
    }
}    