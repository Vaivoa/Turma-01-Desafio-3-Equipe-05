using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using Xunit;

namespace Modalmais.Test.Unitarios
{

    [Collection(nameof(ClienteCollection))]
    public class ClienteTestes
    {
        private readonly ClienteFixtureTestes _clienteFixtureTestes;

        public ClienteTestes(ClienteFixtureTestes clienteFixtureTestes)
        {
            _clienteFixtureTestes = clienteFixtureTestes;
        }

        [Trait("Categoria", "Testes Cliente")]
        [Theory(DisplayName = "Validar CPF")]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("123", true)]
        [InlineData("123456789123456789", true)]
        [InlineData("11122233344", true)]
        [InlineData("276.486.240-79", true)]
        [InlineData("78080103089", false)]
        public void NovoCliente_ValidarCpf_DeveCorresponderAoResultadoEsperado(string cpf, bool resultadoEsperado)
        {
            // Arrange
            var cliente = new Cliente("Beatriz", "Pires", cpf,
                                new Contato(
                                    new Celular(DDDBrasil.AC_RioBranco, "940041211"),
                                    "email@email.com")
                                );

            // Act
            var resultado = cliente.ValidarUsuario();

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Trait("Categoria", "Testes Cliente")]
        [Theory(DisplayName = "Validar E-mail")]
        [InlineData("", true)]
        [InlineData("  ", true)]
        [InlineData("@email.com", true)]
        [InlineData("teste@teste", true)]
        [InlineData("teste@email@email.com", true)]
        [InlineData("joao@gmail.com", false)]
        public void NovoCliente_ValidarEmail_DeveCorresponderAoResultadoEsperado(string email, bool resultadoEsperado)
        {
            // Arrange
            var cliente = new Cliente("Beatriz", "Pires", "78080103089",
                new Contato(
                    new Celular(DDDBrasil.AC_RioBranco, "940041211"),
                    email)
            );

            // Act
            var resultado = cliente.ValidarUsuario();

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Trait("Categoria", "Testes Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente válido")]
        public void NovoCliente_ClienteValido_ValidadorDeveRetornarFalso()
        {
            // Arrange
            var cliente = _clienteFixtureTestes.GerarClienteValido();

            // Act & Assert
            Assert.False(cliente.ValidarUsuario());
        }

        [Trait("Categoria", "Testes Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente inválido")]
        public void NovoCliente_ClienteValido_ValidadorDeveRetornarVerdadeiro()
        {
            // Arrange
            var cliente = _clienteFixtureTestes.GerarClienteIncorreto();

            // Act & Assert
            Assert.True(cliente.ValidarUsuario());
        }
    }
}