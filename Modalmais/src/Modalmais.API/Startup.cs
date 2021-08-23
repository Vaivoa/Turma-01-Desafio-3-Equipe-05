using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.API.Configurations;

namespace Modalmais.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddConfiguracaoApp();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseConfiguracaoApp(env);

        }
    }
}
