using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Services.VirtualMachineUseCases;
using Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Application.WebApp
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(options => { options.EnableEndpointRouting = false; });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            });
            services.AddTransient<SignInManager<User>, SignInManager>();
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddAuthorization();
            services
                .AddIdentity<User, Role>(options => { options.User.RequireUniqueEmail = true; })
                .AddUserStore<UserStore>()
                .AddRoles<Role>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();
            
            
            // Application.Infrastructure
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("api", "api/vm/{id?}");
            });
        }
    }
}