using Application.Core.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure.EFDataAccess
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PostgresContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}