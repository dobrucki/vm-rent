using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Application.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration(config =>
                {
                    config
                        .AddJsonFile("appsettings.json", 
                            optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);
                });
    }
}   