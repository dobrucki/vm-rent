using Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.EfCore
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