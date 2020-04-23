using AutoMapper;
using Core.Application.QueryModel.Customers;
using Core.Application.QueryModel.VirtualMachines;
using Infrastructure.Persistence.Query.Customers;
using Infrastructure.Persistence.Query.VirtualMachines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Query
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationReadDatabase(
            this IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            services.AddTransient<ICustomersQueryRepository, CustomerRepository>();
            services.AddTransient<IVirtualMachinesQueryRepository, VirtualMachineRepository>();
            services.AddMediatR(typeof(DependencyInjection));
            return services;
        }
        
    }
}