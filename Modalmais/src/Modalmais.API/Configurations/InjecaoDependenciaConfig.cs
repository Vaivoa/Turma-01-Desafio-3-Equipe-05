using Microsoft.Extensions.DependencyInjection;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Infra.Data;
using Modalmais.Infra.Repository;

namespace Modalmais.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services)
        {

            services.AddScoped<DbContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }

    }
}
