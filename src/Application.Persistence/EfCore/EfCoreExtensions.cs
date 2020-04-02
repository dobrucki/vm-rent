using Application.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure.EfCore
{
    public static class EfCoreExtensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<PostgresContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}