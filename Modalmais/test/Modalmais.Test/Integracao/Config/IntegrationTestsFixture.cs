using System;
using System.Net.Http;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Modalmais.API.MVC;
using Xunit;

namespace Modalmais.Test.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public string UsuarioEmail;

        public readonly StartUpFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost:5001"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new StartUpFactory<TStartup>();
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