using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.API.Configurations;

namespace Modalmais.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        internal static string _hostEnvironment { get; private set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            _hostEnvironment = hostEnvironment.EnvironmentName;

            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddConfiguracaoApp(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseConfiguracaoApp(env);

        }
    }
}
