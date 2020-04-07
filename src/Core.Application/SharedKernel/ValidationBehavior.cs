using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.SharedKernel
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug("Validation process started.");
            var validationFailures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!validationFailures.Any())
            {
                _logger.LogDebug("Validation process passed.");
                return await next();
            }
            _logger.LogDebug("Validation process failed! Throwing an exception...");

            var errors = new List<Error>();
            validationFailures
                .ForEach(x => 
                    errors.Add(new ValidationError(x.PropertyName, x.ErrorMessage)));
            

            throw new InvalidRequestException("Request did not pass validation process.", errors);
        }
    }
}