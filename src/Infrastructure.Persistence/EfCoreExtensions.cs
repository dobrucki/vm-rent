using Core.Application.Customers;
using Core.Application.SharedKernel;
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
                options.UseNpgsql(connectionString);
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();

            return services;
        }
    }
}