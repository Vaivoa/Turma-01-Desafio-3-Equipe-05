using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Modalmais.Test.Tests.Config
{
    public class ProgramaFactory<Programa> : WebApplicationFactory<Programa> where Programa : class
    {

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            // Add mock/test services to the builder here
       

            return base.CreateHost(builder);
        }

        /* protected override void ConfigureWebHost(IWebHostBuilder builder)
         {
             builder.UseStartup<Programa>();
             builder.UseEnvironment("Testing");
         }*/
    }
}