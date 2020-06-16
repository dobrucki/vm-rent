using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        }
    }
}