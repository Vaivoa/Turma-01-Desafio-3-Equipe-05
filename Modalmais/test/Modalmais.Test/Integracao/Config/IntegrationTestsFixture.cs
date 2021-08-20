using System;
using System.Net.Http;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Modalmais.API.MVC;
using Xunit;

namespace NerdStore.WebApp.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public string UsuarioEmail;

        public readonly LojaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GerarClienteFake()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email(faker.Name.FirstName(),faker.Name.LastName()).ToLower();
        }



        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}