using System;

namespace UserService.Application.Commands
{
    public class DeactivateUserCommand : ICommand
    {
        public Guid Id { get; }
        
        public DeactivateUserCommand(Guid id)
        {
            Id = id;
        }
    }
}