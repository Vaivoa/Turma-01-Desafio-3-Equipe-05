﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modalmais.Transacoes.API.Data;
using System;

namespace Modalmais.Transacoes.API.Configurations
{
    public static class ConfiguracaoApp
    {
        public static IServiceCollection AddConfiguracaoApp(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Api-StringBd-Postgres"), o =>
                {
                    o.EnableRetryOnFailure();
                }).UseAllCheckConstraints();
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{configuration.GetConnectionString("Api-StringBd-Redis")}";
            });

            services.InjecaoDependencias(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AdicionarConfiguracaoSwagger();

            return services;
        }

        public static IApplicationBuilder UseConfiguracaoApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguracaoSwagger();

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
