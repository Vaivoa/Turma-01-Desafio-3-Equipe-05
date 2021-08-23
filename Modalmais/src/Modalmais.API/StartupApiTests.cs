using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.Infra.Data;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Services.Request;
using Modalmais.Business.Services.Response;
using Modalmais.Infra.Repository;
using Modalmais.Business.Notificador;
using Microsoft.Extensions.Hosting;

namespace Modalmais.API.MVC
{
    public class StartupApiTests
    {
        public IConfiguration Configuration { get; }
        

        public StartupApiTests(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(p => new DbContext(
                Configuration.GetConnectionString("Api-StringBd-Mongodb").ToString(),
                Configuration.GetConnectionString("NomeApiDb").ToString()
                ));
            services.AddScoped<INotificador, NotificadorHandler>();
            //Repositorys
            services.AddScoped<IClienteRepository, ClienteRepository>();

            //Services
            services.AddScoped<IClienteServiceResponse, ClienteServiceResponse>();
            services.AddScoped<IClienteServiceRequest, ClienteServiceRequest>();

            services.AddControllers();
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
