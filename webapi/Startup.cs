using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using webapi.DAL;
using webapi.DAL.repos;
using Newtonsoft.Json.Serialization;

namespace webapi
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
      services.AddMvc()
        .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
      services.AddDbContext<ApplicationContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddAuthentication(sharedOptions =>
        {
          sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddAzureAdB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

      services.AddScoped<WorkoutProgramRepo, WorkoutProgramRepo>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseDefaultFiles();
      app.UseAuthentication();
      app.UseStaticFiles();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}");
      });
    }
  }
}
