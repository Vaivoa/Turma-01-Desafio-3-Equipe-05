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

namespace Modalmais.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services)
        {
            //DbContext
            services.AddScoped<DbContext>();
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
