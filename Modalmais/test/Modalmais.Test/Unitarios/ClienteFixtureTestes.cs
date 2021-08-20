using System;
using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
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
            var genero = new Faker().PickRandom<Name.Gender>();
            var ddd = new Faker().PickRandom<DDDBrasil>();
            var numero = new Faker().Random.Number(900000000, 999999999).ToString();

            var clienteValido = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Person.Cpf(false),
                    new Contato(new Celular(ddd, numero), f.Internet.ExampleEmail())
                ));

            return clienteValido;
        }

        public Cliente GerarClienteIncorreto()
        {
            var clienteIncorreto = new Cliente(
                "",
                "",
                "",
                new Contato(new Celular(DDDBrasil.SP_Bauru, ""), "")
            );

            return clienteIncorreto;
        }

        public void Dispose()
        {
        }
    }
}