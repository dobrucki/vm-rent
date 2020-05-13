using AutoMapper;
using RentService.Core.Application.CommandModel.Customers;
using RentService.Core.Application.CommandModel.Rentals;
using RentService.Core.Application.CommandModel.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RentService.Infrastructure.Persistence
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
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IVirtualMachineRepository, VirtualMachineRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();

            services.AddAutoMapper(typeof(PersistenceExtensions));
            return services;
        }

        // public static IServiceCollection AddApplicationReadDatabase(this IServiceCollection services,
        //     string connectionString)
        // {
        //     services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        //     services.AddTransient<ICustomersQueryRepository, ReadCustomersRepository>();
        //     services.AddMediatR(typeof(PersistenceExtensions));
        //     return services;
        // }
    }
}