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
            services.AddScoped(p => new MongoDbContext(
                configuration.GetConnectionString("Api-StringBd-Mongodb").ToString(),
                configuration.GetConnectionString("NomeApiDb").ToString()
                ));
            services.AddScoped<INotificador, NotificadorHandler>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteServiceResponse, ClienteServiceResponse>();
            services.AddScoped<IClienteServiceRequest, ClienteServiceRequest>();

            services.AddScoped(p => new KafkaProducerHostedService(
                configuration.GetConnectionString("Api-StringBd-Kafka").ToString()));

            return services;
        }

    }
}
