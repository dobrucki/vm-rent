using System;

namespace UserService.Core.Application.SharedKernel.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message)
        {
        }
    }
}