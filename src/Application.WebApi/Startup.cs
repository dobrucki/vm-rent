using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;
using Application.Core.Services.VirtualMachineUseCases;
using Application.Infrastructure.EFDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft;

namespace Application.WebApi
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
            services.AddControllers()
                .AddNewtonsoftJson();
            

            // Application.Infrastructure
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//            services.AddDbContext<PostgresContext>(options =>
//            {
//                options.UseNpgsql(Configuration.GetConnectionString("PostgresContext"));
//            });
//            services.AddTransient<IUnitOfWork>(unitOfWork => new UnitOfWork(new PostgresContext()));
//            
//            // Application.Infrastructure.EFDataAccess
//            services.AddDbContext<PostgresContext>();
            services.AddPostgres(Configuration.GetConnectionString("PostgresContext"));
            
            // Services
            services.AddMediatR(typeof(DeleteVirtualMachineHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}