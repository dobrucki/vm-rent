using System;

namespace UserService.Core.Application.CommandModel.Roles.Commands
{
    public class CreateRoleCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}