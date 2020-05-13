using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = RentService.Core.Application.SharedKernel.Exceptions.ValidationException;

namespace RentService.Core.Application.CommandModel
{
    internal sealed class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<CommandValidationBehavior<TRequest, TResponse>> _logger;

        public CommandValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<CommandValidationBehavior<TRequest, TResponse>> logger)
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

            if (validationFailures.Any())
            {
                throw new SharedKernel.Exceptions.ValidationException(validationFailures);
            }

            return await next();
        }
    }
}