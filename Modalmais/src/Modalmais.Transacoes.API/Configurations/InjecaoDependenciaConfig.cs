using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Notificador;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.Refit;
using Modalmais.Transacoes.API.Repository;
using Refit;
using System;

namespace Modalmais.Transacoes.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IContaService>().ConfigureHttpClient(c =>
            {

                c.BaseAddress = new Uri($"{configuration.GetConnectionString("ConexaoRefit")}");
            });

            services.AddScoped<ApiDbContext>();
            services.AddScoped<INotificador, NotificadorHandler>();
            services.AddScoped<TransacaoRepository>();

            return services;
        }

    }
}
