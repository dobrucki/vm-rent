namespace UserService.Infrastructure.Persistence.Query
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationReadDatabase(
            this IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            services.AddTransient<ICustomersQueryRepository, UserRepository>();
            services.AddTransient<IVirtualMachinesQueryRepository, RoleRepository>();
            services.AddTransient<IRentalsQueryRepository, RentalRepository>();
            services.AddMediatR(typeof(DependencyInjection));
            return services;
        }
        
    }
}