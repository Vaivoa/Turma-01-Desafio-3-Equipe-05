using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Modalmais.API.Configurations
{
    public static class ConfiguracaoApp
    {


        public static IServiceCollection AddConfiguracaoApp(this IServiceCollection services)
        {

            services.InjecaoDependencias();

            services.AddControllers();
            services.AdicionarConfiguracaoSwagger();

            return services;
        }

        public static IApplicationBuilder UseConfiguracaoApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseConfiguracaoSwagger();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

    }
}
