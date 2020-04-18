using AutoMapper;
using Core.Application.Customers;
using Core.Application.Rentals;
using Core.Application.SharedKernel;
using Core.Application.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class EfCoreExtensions
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

            services.AddAutoMapper(typeof(EfCoreExtensions));
            return services;
        }
    }
}