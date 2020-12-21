using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.IO;

namespace ApiGateway
{
  class Program
  {
    static void Main(string[] args)
    {
      new WebHostBuilder()
         .UseKestrel()
         .UseContentRoot(Directory.GetCurrentDirectory())
         .ConfigureAppConfiguration((hostingContext, config) =>
         {
           config
                     .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                     .AddJsonFile("appsettings.json", false, true)
                     .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();
         })
         .ConfigureServices(s =>
         {
           s.AddOcelot();
         })
         .ConfigureLogging((hostingContext, logging) =>
         {
                 //add your logging
               })
         .Configure(app =>
         {
           app.UseOcelot();
         })
         .Build()
         .Run();
    }
  }
}
