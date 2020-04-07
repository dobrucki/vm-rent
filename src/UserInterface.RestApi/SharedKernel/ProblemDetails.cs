using System.Collections.Generic;
using Core.Application.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.RestApi.SharedKernel
{
    public class InvalidRequestProblemDetails : ProblemDetails
    {
        public IEnumerable<Error> Errors { get; }

        public InvalidRequestProblemDetails(InvalidRequestException exception)
        {
            Title = "validation-error";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Errors = exception.Errors;
            Type = "http://localhost/errors/validation-error";
        }
    }
}    