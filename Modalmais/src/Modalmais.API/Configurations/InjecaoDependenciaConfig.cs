using Microsoft.Extensions.DependencyInjection;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Services.Request;
using Modalmais.Business.Services.Response;
using Modalmais.Business.Notificador;
using Modalmais.Infra.Data;
using Modalmais.Infra.Repository;
using Microsoft.Extensions.Configuration;

namespace Modalmais.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services, IConfiguration Configuration, string hostEnvironment)
        {
            //DbContext
            if (hostEnvironment == "Testing") 
            {
                services.AddScoped(p => new DbContext(
                Configuration.GetConnectionString("Api-StringBd-Mongodb").ToString(),
                Configuration.GetConnectionString("NomeApiDb").ToString()
                ));
            }
            else
            {
                services.AddScoped<DbContext>();
            }
            //NotificationPattern
            services.AddScoped<INotificador, NotificadorHandler>();
            //Repositorys
            services.AddScoped<IClienteRepository, ClienteRepository>();

            //Services
            services.AddScoped<IClienteServiceResponse, ClienteServiceResponse>();
            services.AddScoped<IClienteServiceRequest, ClienteServiceRequest>();

            return services;
        }

    }
}
