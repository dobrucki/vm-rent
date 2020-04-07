using System;
using System.Collections.Generic;

namespace Core.Application.SharedKernel
{
    public class InvalidRequestException : Exception
    {
        public IEnumerable<Error> Errors { get; }
        public InvalidRequestException(string message, IEnumerable<Error> errors) : base(message)
        {
            Errors = errors;
        }    
    }

}    