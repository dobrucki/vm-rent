using System;

namespace RentService.Core.Application.SharedKernel.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message)
        {
        }
    }
}