using Modalmais.Business.Models;
using Modalmais.Business.Models.Enums;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using Xunit;

namespace Modalmais.Test.Unitarios
{
    public class ClienteTestes
    {
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

    }
}