using AutoMapper;
using RentService.Core.Application.QueryModel.Customers;
using RentService.Core.Application.QueryModel.Rentals;
using RentService.Core.Application.QueryModel.VirtualMachines;
using Infrastructure.Persistence.Query.Customers;
using Infrastructure.Persistence.Query.Rentals;
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
            services.AddTransient<IRentalsQueryRepository, RentalRepository>();
            services.AddMediatR(typeof(DependencyInjection));
            return services;
        }
        
    }
}