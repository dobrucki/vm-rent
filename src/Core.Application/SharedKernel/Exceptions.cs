using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Application.SharedKernel
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message) : base(message)
        {
        }
    }

    public class RequestValidationException : InvalidRequestException
    {
        public IEnumerable<object> Errors { get; }
        public RequestValidationException(string message, IEnumerable<object> errors) : base(message)
        {
            Errors = errors;
        }
    }
}