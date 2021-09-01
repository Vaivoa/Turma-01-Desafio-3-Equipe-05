using System.Threading.Tasks;
using Refit;

namespace Modalmais.Transacoes.API.Refit
{
    public interface IContaService
    {
        [Get("/clientes/contas/chavepix")]
        public Task<RespostaConta> ObterConta([AliasAs("chave")] string chave, [AliasAs("tipo")] string tipo);
    }
}