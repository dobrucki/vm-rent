using System.Reflection;
using AutoMapper;
using RentService.Core.Application.CommandModel;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RentService.Core.Application.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddTransient(typeof(IPipelineBehavior<,>), 
                // typeof(CommandValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}