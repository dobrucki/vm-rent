using System.Reflection;
using Core.Application.Customers.CreateCustomer;
using Core.Application.Customers.EditCustomerDetails;
using Core.Application.Customers.GetCustomer;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // TODO: Use SimpleInjector as  IoC container.
            services.AddTransient(
                typeof(IValidator<GetCustomerQuery>), 
                typeof(GetCustomerQueryValidator));
            services.AddTransient(
                typeof(
                    IValidator<CreateCustomerCommand>), 
                typeof(CreateCustomerCommandValidator));
            services.AddTransient(
                typeof(
                    IValidator<EditCustomerDetailsCommand>), 
                typeof(
                    EditCustomerDetailsCommandValidator));
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(
                    ValidationBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}