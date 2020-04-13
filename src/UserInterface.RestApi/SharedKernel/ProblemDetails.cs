using System.Collections.Generic;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.RestApi.SharedKernel
{
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = "validation-error";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Type = "http://localhost/errors/validation-error";
        }
    }

    public class NotFoundProblemDetails : ProblemDetails
    {
        public NotFoundProblemDetails(NotFoundException exception)
        {
            Title = "resource-not-found-error";
            Status = StatusCodes.Status404NotFound;
            Detail = exception.Message;
            Type = "http://localhost/errors/resource-not-found-error";
        }
    }

    public class ValidationProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationProblemDetails(ValidationException exception)
        {
            Title = "validation-error";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Type = "http://localhost/errors/validation-error";
            Errors = exception.ValidationErrors;
        }
    }
}    