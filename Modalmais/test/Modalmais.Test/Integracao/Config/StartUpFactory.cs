using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Modalmais.Test.Tests.Config
{
    public class StartUpFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Testing");
            builder.UseSetting("anonymousAuthentication", "true");
        }

    }
}