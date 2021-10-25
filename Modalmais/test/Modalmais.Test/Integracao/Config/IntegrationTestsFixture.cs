using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modalmais.API;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Core.Models.Enums;
using Modalmais.Infra.Data;
using Modalmais.Transacoes.API;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.Refit;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Modalmais.Test.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable
        where TStartup : class
    {
        public static string UsuarioEmail;

        public readonly StartUpFactory<TStartup> Factory;
        public HttpClient Client;
        public WebApplicationFactory<StartupTransacaoApiTests> FactoryTransacao;
        public HttpClient ClientTransacao;

        public readonly WebApplicationFactoryClientOptions clientCadastroOptions = new()
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("http://localhost:5000"),
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        };

        public readonly WebApplicationFactoryClientOptions clientTransacaoOptions = new()
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("http://localhost:5100"),
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        };

        public int ContadorTransferencias { get; set; }

        public IntegrationTestsFixture()
        {
            Factory = new StartUpFactory<TStartup>();
            Client = Factory.CreateClient(clientCadastroOptions);
            FactoryTransacao = new WebApplicationFactory<StartupTransacaoApiTests>().WithWebHostBuilder(a =>
            {
                a.ConfigureServices(e =>
                {
                    e.AddHttpClient<IContaService, ObterContaMock>((d) => new ObterContaMock(BuscarConta()));
                });

                a.ConfigureAppConfiguration((c, b) =>
                {
                    c.HostingEnvironment.EnvironmentName = "Testing";
                });
            });
            ClientTransacao = FactoryTransacao.CreateClient(clientTransacaoOptions);

            LimpandoDatabaseMongoTestes();
            LimpandoDatabasePostgresTestes();
        }

        private void LimpandoDatabaseMongoTestes()
        {
            var _context = new MongoDbContext("mongodb://localhost:27017", "Testes");
            _context.Clientes.DeleteMany(new BsonDocument());
        }

        private void LimpandoDatabasePostgresTestes()
        {
            var dbContextOptions = new
                DbContextOptionsBuilder<ApiDbContext>()
                .UseNpgsql("Host=localhost;Database=modalmaisTeste;Username=postgres;Password=rootvaivoa")
                .UseAllCheckConstraints()
                .Options;
            var _contextTransacao = new ApiDbContext(dbContextOptions);
            _contextTransacao.Database.EnsureDeletedAsync().Wait();
            _contextTransacao.Database.MigrateAsync().Wait();
            _contextTransacao.Database.EnsureCreatedAsync().Wait();
        }

        public async Task<ClienteResponse> BuscarUsuario()
        {
            var getResponse = await Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];
            return cliente;
        }

        public async Task<RespostaConta> BuscarConta()
        {
            var usuario = BuscarUsuario().Result;
            string usuarioChavePix = usuario.ContaCorrente.ChavePix.Chave;
            string usuarioTipoChave = $"{(int)usuario.ContaCorrente.ChavePix.Tipo}";

            var getResponse = await Client.GetAsync($"api/v1/clientes/contas/chavepix?chave={usuarioChavePix}&tipo={usuarioTipoChave}");
            var clientesResponse = JsonConvert.DeserializeObject
                    <RespostaConta>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse;
            return cliente;
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
            Client?.Dispose();
            Factory?.Dispose();
        }
    }
}