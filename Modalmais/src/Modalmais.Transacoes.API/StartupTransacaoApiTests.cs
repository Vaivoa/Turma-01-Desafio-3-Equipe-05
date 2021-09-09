using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Notificador;
using Modalmais.Transacoes.API.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.Repository;
using System;

namespace Modalmais.Transacoes.API
{
    public class StartupTransacaoApiTests { 
        public StartupTransacaoApiTests(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Api-StringBd-Postgres"), o =>
                {
                    o.EnableRetryOnFailure();
                }).UseAllCheckConstraints();
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration.GetConnectionString("Api-StringBd-Redis")}";
            });

            services.AddScoped<ApiDbContext>();
            services.AddScoped<INotificador, NotificadorHandler>();
            services.AddScoped<TransacaoRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           /* services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });*/

            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
