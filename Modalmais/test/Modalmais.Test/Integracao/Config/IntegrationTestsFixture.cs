﻿using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc.Testing;
using Modalmais.API.MVC;
using Modalmais.Business.Models;
using Modalmais.Core.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Infra.Data;
using MongoDB.Bson;
using System;
using System.Net.Http;
using Xunit;
using Modalmais.Transacoes.API;
using Modalmais.Transacoes.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Modalmais.Test.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public static string UsuarioEmail;

        public readonly StartUpFactory<TStartup> Factory;
        public HttpClient Client;
        public readonly StartUpFactory<StartupTransacaoApiTeste> FactoryTransacao;
        public HttpClient ClientTransacao;

        public readonly MongoDbContext _context;

        public readonly ApiDbContext _contextTransacao;

        public IntegrationTestsFixture()
        {
            var clientCadastroOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:5000/"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };
            var clientTransacaoOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:5100/"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            _context = new MongoDbContext("mongodb://localhost:27017", "Testes");
            _context.Clientes.DeleteMany(new BsonDocument());
            var dbContextOptions = new 
                DbContextOptionsBuilder<ApiDbContext>()
                .UseNpgsql("Host=localhost;Database=modalmaisTeste;Username=postgres;Password=rootvaivoa")
                .UseAllCheckConstraints()
                .Options;

            _contextTransacao = new ApiDbContext(dbContextOptions);
            _contextTransacao.Database.EnsureDeleted();
            _contextTransacao.Database.Migrate();

            Factory = new StartUpFactory<TStartup>();
            Client = Factory.CreateClient(clientCadastroOptions);
            FactoryTransacao = new();
            ClientTransacao = FactoryTransacao.CreateClient(clientTransacaoOptions);
            
        }

        public static string GerarClienteEmailFake()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email(faker.Name.FirstName(), faker.Name.LastName()).ToLower();
            return UsuarioEmail;
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