using System;
using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
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
            var clienteValido = new Cliente(
                "Ricardo",
                "Camargo",
                "48751466040",
                new Contato(
                    new Celular(DDDBrasil.AM_Manaus, "789855547"),
                    "email@email.com"
                )
            );

            return clienteValido;
        }

        public void Dispose()
        {
        }
    }
}