using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Notificador;
using Modalmais.Transacoes.API.Data;

namespace Modalmais.Transacoes.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddScoped<ApiDbContext>();

            //NotificationPattern
            services.AddScoped<INotificador, NotificadorHandler>();

            //Repositorys


            //Services


            return services;
        }

    }
}
