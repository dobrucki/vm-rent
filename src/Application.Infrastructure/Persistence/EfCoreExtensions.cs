using Application.Core.Models;
using Application.Core.Ports;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure.Persistence
{
    public static class EfCoreExtensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PostgresContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUserStore<User>, UserStore>();
            services.AddIdentityCore<User>()
                .AddUserStore<UserStore>();

            return services;
        }
    }
}