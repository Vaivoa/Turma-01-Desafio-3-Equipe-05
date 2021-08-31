using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Modalmais.Transacoes.API.Configurations
{
    public static class ConfiguracaoSwagger
    {


        public static void AdicionarConfiguracaoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API - Modalmais",
                    Description = "Realiza transações entre contas.",
                    Contact = new OpenApiContact() { Name = "Equipe 5", Email = "equipe5@vaivoa.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

            });
        }


        public static void UseConfiguracaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

    }
}
