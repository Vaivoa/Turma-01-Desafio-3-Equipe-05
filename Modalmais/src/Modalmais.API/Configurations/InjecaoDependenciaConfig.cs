using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Services.Request;
using Modalmais.Business.Services.Response;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Notificador;
using Modalmais.Infra.Data;
using Modalmais.Infra.Repository;

namespace Modalmais.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddScoped(p => new DbContext(
                configuration.GetConnectionString("Api-StringBd-Mongodb").ToString(),
                configuration.GetConnectionString("NomeApiDb").ToString()
                ));
            //NotificationPattern
            services.AddScoped<INotificador, NotificadorHandler>();
            //Repositorys
            services.AddScoped<IClienteRepository, ClienteRepository>();

            //Services
            services.AddScoped<IClienteServiceResponse, ClienteServiceResponse>();
            services.AddScoped<IClienteServiceRequest, ClienteServiceRequest>();

            services.AddScoped(p => new KafkaProducerHostedService(
                configuration.GetConnectionString("Api-StringBd-Kafka").ToString()));

            return services;
        }

    }
}
