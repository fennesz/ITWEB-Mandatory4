using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using webapi.DAL;
using webapi.DAL.repos;

namespace webapi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // If people forget to set a Secret environment variable
      var secret = Environment.GetEnvironmentVariable("Secret");
      if (secret == null) throw new Exception("The application requires a ''Secret'' environment variable");

      var host = BuildWebHost(args);

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationContext>();
        var repo = services.GetRequiredService<WorkoutProgramRepo>();
        DbInitializer.Initialize(context, repo);
      }


      host.Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build();
  }
}
