using AutoMapper;
using Core.Application.QueryModel.Customers;
using Infrastructure.Persistence.Query.Customers;
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
            services.AddMediatR(typeof(DependencyInjection));
            return services;
        }
        
    }
}