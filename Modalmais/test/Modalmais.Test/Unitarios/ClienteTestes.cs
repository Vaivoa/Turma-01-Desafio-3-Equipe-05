using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
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
            var cliente = new Cliente("Beatriz", "Pires",
                                new Contato(
                                    new Celular(DDDBrasil.AC_RioBranco, "940041211"),
                                    "email@email.com"),
                                new Documento(cpf)
                                );

            // Act
            var resultado = cliente.EstaInvalido();

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
            var cliente = new Cliente("Beatriz", "Pires",
                new Contato(
                    new Celular(DDDBrasil.AC_RioBranco, "940041211"),
                    email),
               new Documento("78080103089")
            );

            // Act
            var resultado = cliente.EstaInvalido();

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
            Assert.False(cliente.EstaInvalido());
        }

        [Trait("Categoria", "Testes Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente inválido")]
        public void NovoCliente_ClienteValido_ValidadorDeveRetornarVerdadeiro()
        {
            // Arrange
            var cliente = _clienteFixtureTestes.GerarClienteIncorreto();

            // Act & Assert
            Assert.True(cliente.EstaInvalido());
        }

        //PIX
        [Trait("Categoria", "Testes Cliente")]
        [Theory(DisplayName = "Validar criação de uma chave pix válida")]
        [InlineData(null, TipoChavePix.Aleatoria)]
        [InlineData("usuario@valido.com", TipoChavePix.Email)]
        [InlineData("99999999999", TipoChavePix.Telefone)]
        [InlineData("", TipoChavePix.CPF)]

        public void NovaChavePix_ChavePixValida_ValidadorDeveRetornarFalso(string chave, TipoChavePix tipo)
        {
            // Arrange
            var cliente = _clienteFixtureTestes.GerarClienteValido();
            if (tipo == TipoChavePix.CPF) chave = cliente.Documento.CPF;
            var chavePix = new ChavePix(chave, tipo);
            cliente.ContaCorrente.AdicionarChavePix(chavePix);

            // Act & Assert
            var a = cliente.ContaCorrente.ChavePix.EstaInvalido();
            var b = cliente.ContaCorrente.ChavePix;
            Assert.False(cliente.ContaCorrente.ChavePix.EstaInvalido());
        }

        [Trait("Categoria", "Testes Cliente")]
        [Theory(DisplayName = "Validar criação de uma chave pix inválida")]
        [InlineData("usuariovalido.com", TipoChavePix.Email)]
        [InlineData("999999999910", TipoChavePix.Telefone)]
        [InlineData("675661360034", TipoChavePix.CPF)]

        public void NovaChavePix_ChavePixInvalida_ValidadorDeveRetornarVerdadeiro(string chave, TipoChavePix tipo)
        {
            // Arrange
            var cliente = _clienteFixtureTestes.GerarClienteValido();
            var chavePix = new ChavePix(chave, tipo);
            cliente.ContaCorrente.AdicionarChavePix(chavePix);

            // Act & Assert
            Assert.True(cliente.ContaCorrente.ChavePix.EstaInvalido());
        }

    }
}