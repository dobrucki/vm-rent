using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using RentService.Core.Application.SharedKernel;
using RentService.Infrastructure.Persistence;
using RentService.Infrastructure.Persistence.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoapCore;
using UserInterface.SoapApi.Services;

// using RentService.UserInterface.SoapApi.Services;

namespace UserInterface.SoapApi
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
            // services.TryAddSingleton<ICustomerService, CustomerService>();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddSoapCore();
            services.AddApplicationServices();
            services.AddApplicationDatabase(Configuration.GetConnectionString("PostgresContext"));
            services.AddApplicationReadDatabase("mongodb://root:rootpassword@rodb:27017");
            services.AddTransient<ICustomerService, CustomerService>();
            
            services.AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSoapEndpoint<ICustomerService>("/CustomerService.svc", new BasicHttpBinding(),
                SoapSerializer.DataContractSerializer);
            app.UseSoapEndpoint<ICustomerService>("/CustomerService.asmx", new BasicHttpBinding(),
                SoapSerializer.XmlSerializer);

            app.UseMvc();
        }
    }
}