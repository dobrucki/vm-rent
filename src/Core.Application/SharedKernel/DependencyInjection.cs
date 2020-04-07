using System.Reflection;
using Core.Application.Customers.GetCustomer;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IValidator<GetCustomerQuery>), typeof(GetCustomerQueryValidator));
            services.AddTransient(typeof(IValidator<GetCustomerQuery>), typeof(GetCustomerQueryValidator1));
            // services.AddTransient(typeof(IRequestExceptionHandler<,>), typeof(ValidationExceptionHandler<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}