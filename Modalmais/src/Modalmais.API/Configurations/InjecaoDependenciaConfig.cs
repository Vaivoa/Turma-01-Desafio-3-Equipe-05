using Microsoft.Extensions.DependencyInjection;
using Modalmais.Infra.Data;

namespace Modalmais.API.Configurations
{
    public static class InjecaoDependenciaConfig
    {

        public static IServiceCollection InjecaoDependencias(this IServiceCollection services)
        {

            services.AddScoped<DbContext>();

            return services;
        }

    }
}
