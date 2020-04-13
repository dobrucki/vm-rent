using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Core.Application.SharedKernel.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; }
 
        public ValidationException()
            : base("One or more validation error have occurred.")
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(x => x.PropertyName)
                .Distinct();
            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(x => x.PropertyName == propertyName)
                    .Select(x => x.ErrorMessage)
                    .ToArray();
                
                ValidationErrors.Add(propertyName, propertyFailures);
            }
        }
    }
}