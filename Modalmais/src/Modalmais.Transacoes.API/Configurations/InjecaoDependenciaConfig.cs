using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Notificador;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.Repository;

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
            services.AddScoped<TransacaoRepository>();

            //Services


            return services;
        }

    }
}
