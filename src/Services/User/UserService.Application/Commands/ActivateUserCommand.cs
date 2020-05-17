using System;

namespace UserService.Application.Commands
{
    public class ActivateUserCommand : ICommand
    {
        public Guid Id { get; }

        public ActivateUserCommand(Guid id)
        {
            Id = id;
        }
    }
}