using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserService.Application.Commands;
using UserService.Application.IntegrationEvents;
using UserService.Domain.Models.UserAggregate;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient<IEventBus, EventBusRabbitMq>();
            services.AddMediatR(typeof(ActivateUserCommandHandler).Assembly);
            
            services.AddDbContext<UserServiceContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("UserServiceContext")));
            
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                // var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                // var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                // var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                
                return new EventBusRabbitMq(eventBusSubcriptionsManager, 
                    sp.GetRequiredService<ILogger<EventBusRabbitMq>>());
            });
            
            
        }

        private void InitializeDb(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<UserServiceContext>().Database.Migrate();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            InitializeDb(app);
        }
    }
}