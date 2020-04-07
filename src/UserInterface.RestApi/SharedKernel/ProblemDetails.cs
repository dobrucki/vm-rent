using System.Collections.Generic;
using Core.Application.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.RestApi.SharedKernel
{
    public class RequestValidationProblemDetails : ProblemDetails
    {
        public IEnumerable<object> Errors { get; set; }
        
        public static RequestValidationProblemDetails Create(
            RequestValidationException exception, HttpContext context)
        {
            var problem = new RequestValidationProblemDetails
            {
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                Title = "request-validation-error",
                Instance = context.Request.Path.Value,
                Type = "about:blank",
                Errors = exception.Errors
            };
            return problem;
        }
    }
}