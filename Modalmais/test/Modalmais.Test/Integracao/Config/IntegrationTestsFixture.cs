using System;
using System.Net.Http;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc.Testing;
using Modalmais.API.MVC;
using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Test.Unitarios;
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
                BaseAddress = new Uri("http://localhost:5001/"),
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
        public Cliente GerarClienteValido()
        {
            var faker = new Faker("pt_BR");
            var genero = faker.PickRandom<Name.Gender>();
            var ddd = faker.PickRandom<DDDBrasil>();
            var numero = faker.Random.Number(900000000, 999999999).ToString();
            var nome = faker.Name.FirstName(genero);
            var sobrenome = faker.Name.LastName(genero);
            var clienteValido = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    nome,
                    sobrenome,
                    new Contato(new Celular(ddd, numero), f.Internet.ExampleEmail(nome, sobrenome)),
                    new Documento(f.Person.Cpf(false))
                ));

            return clienteValido;
        }

        public Cliente GerarClienteIncorreto()
        {
            var clienteIncorreto = new Cliente(
                "",
                "",
                new Contato(new Celular(DDDBrasil.SP_Bauru, ""), ""),
                new Documento("")
            );

            return clienteIncorreto;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}