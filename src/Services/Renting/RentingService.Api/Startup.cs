using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
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
using RentingService.Application.Commands;
using RentingService.Application.IntegrationEvents;
using RentingService.Application.IntegrationEvents.Events;
using RentingService.Application.IntegrationEvents.Handlers;
using RentingService.Application.Queries;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Infrastructure;
using RentingService.Infrastructure.Repositories;

namespace RentingService.Api
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient(typeof(IRentalRepository), typeof(RentalRepository));
            services.AddTransient(typeof(IVirtualMachineRepository), typeof(VirtualMachineRepository));
            services.AddTransient(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddMediatR(typeof(CreateRentalCommandHandler).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<VirtualMachineQueries>();
            
            services.AddDbContext<RentingServiceContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("RentingServiceContext")));
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterType<UserCreatedIntegrationEventHandler>();
            var xd = containerBuilder.Build().BeginLifetimeScope("xd");
            // Rabbit
            services.AddTransient<UserCreatedIntegrationEventHandler>();
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                // var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                
                return new EventBusRabbitMq(eventBusSubcriptionsManager, logger, xd);
            });
            
        }

        private void InitializeDb(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<RentingServiceContext>().Database.Migrate();
        }
        
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            InitializeDb(app);
            ConfigureEventBus(app);
        }
    }
}