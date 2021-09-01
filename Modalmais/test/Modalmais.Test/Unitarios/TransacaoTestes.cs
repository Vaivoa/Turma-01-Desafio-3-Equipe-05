using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Modalmais.Transacoes.API.Models;
using Xunit;

namespace Modalmais.Test.Unitarios
{

    [Collection(nameof(ClienteCollection))]
    public class TransacaoTestes
    {

        public TransacaoTestes( )
        {
        }

        [Trait("Categoria", "Testes Transacoes")]
        [Theory(DisplayName = "Validar Transacao")]
        [ClassData(typeof(TransacaoClassData))]
        public void NovoTransacao_ValidarEntidade_DeveCorresponderAoResultadoEsperado(Transacao transacao, bool resultadoEsperado)
        {
            // Arrange && Act
            var resultado = transacao.EstaInvalido();

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            if (!resultado)
                Assert.Empty(transacao.ListaDeErros);
            if (resultado)
                Assert.NotEmpty(transacao.ListaDeErros);
        }


    }
}