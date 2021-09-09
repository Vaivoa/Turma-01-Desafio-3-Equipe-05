using Modalmais.API;
using Modalmais.Transacoes.API.Refit;
using Refit;
using System.Threading.Tasks;
using Xunit;

namespace Modalmais.Test.Tests.Config
{
    internal class ObterContaMock : IContaService
    {

        private readonly RespostaConta _respostaConta;

        public ObterContaMock(RespostaConta respostaConta)
        {
            _respostaConta = respostaConta;
        }

        public async Task<RespostaConta> ObterConta([AliasAs("chave")] string chave, [AliasAs("tipo")] string tipo)
        {

            return _respostaConta;
        }
    }
}