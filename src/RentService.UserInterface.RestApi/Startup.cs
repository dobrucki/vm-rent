using System.ServiceModel;
using RentService.Core.Application.SharedKernel;
using RentService.Core.Application.SharedKernel.Exceptions;
using Hellang.Middleware.ProblemDetails;
using RentService.Infrastructure.Persistence;
using RentService.Infrastructure.Persistence.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using UserInterface.RestApi.SharedKernel;
using UserInterface.SoapApi.Services;

namespace UserInterface.RestApi
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

            services.AddApplicationDatabase(Configuration.GetConnectionString("PostgresContext"));
            services.AddApplicationReadDatabase("mongodb://root:rootpassword@rodb:27017");
            
            services.AddApplicationServices();
            //DependencyInjection.RegisterApplication(_container);

            services.AddProblemDetails(options =>
            {
                options.Map<InvalidCommandException>(exception => new InvalidCommandProblemDetails(exception));
                options.Map<NotFoundException>(exception => new NotFoundProblemDetails(exception));
                options.Map<ValidationException>(exception => new ValidationProblemDetails(exception));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                
                //app.UseDeveloperExceptionPage();
                app.UseProblemDetails();
            }
            else
            {
                app.UseHsts();
                app.UseProblemDetails();
            }

            //app.UseHttpsRedirection();
            context.Database.Migrate();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}