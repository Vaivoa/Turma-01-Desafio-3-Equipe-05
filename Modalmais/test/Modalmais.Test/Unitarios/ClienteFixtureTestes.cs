using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Core.Models.Enums;
using System;
using Xunit;

namespace Modalmais.Test.Unitarios
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteFixtureTestes>
    { }

    public class ClienteFixtureTestes : IDisposable
    {
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
        }
    }
}