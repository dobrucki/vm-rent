using AutoMapper;
using Core.Application.Customers;
using Core.Application.QueryModel.Customers;
using Core.Application.Rentals;
using Core.Application.SharedKernel;
using Core.Application.VirtualMachines;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddApplicationDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<IVirtualMachinesRepository, VirtualMachinesRepository>();
            services.AddTransient<IRentalsRepository, RentalsRepository>();

            services.AddAutoMapper(typeof(PersistenceExtensions));
            return services;
        }

        public static IServiceCollection AddApplicationReadDatabase(this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            services.AddTransient<ICustomersQueryRepository, ReadCustomersRepository>();
            services.AddMediatR(typeof(PersistenceExtensions));
            return services;
        }
    }
}