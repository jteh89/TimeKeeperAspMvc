using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// Necessary for Entity Framework
using Microsoft.Extensions.DependencyInjection;
using TimeKeeperAspMvc.Data;

// Necessary for Autofac
using Autofac;
using TimeKeeperAspMvc.Services;
using TimeKeeperAspMvc.Controllers;

namespace TimeKeeperAspMvc
{
    public class Program
    {
        public static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TimeService>().As<ITimeService>();
            Container = builder.Build();

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TimeKeeperContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
