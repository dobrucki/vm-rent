using System;

namespace UserService.Application.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public Guid Id { get; }
        public string Login { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string Password { get; }

        public RegisterUserCommand(Guid id, string login, string firstName, string lastName, string emailAddress, string password)
        {
            Id = id;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}