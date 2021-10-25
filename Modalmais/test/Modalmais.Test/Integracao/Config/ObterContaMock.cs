using Modalmais.Transacoes.API.Refit;
using Refit;
using System.Threading.Tasks;

namespace Modalmais.Test.Tests.Config
{
    internal class ObterContaMock : IContaService
    {
        private readonly Task<RespostaConta> _respostaConta;

        public ObterContaMock(Task<RespostaConta> respostaConta)
        {
            _respostaConta = respostaConta;
        }

        public async Task<RespostaConta> ObterConta([AliasAs("chave")] string chave, [AliasAs("tipo")] string tipo)
        {
            return await _respostaConta;
        }
    }
}