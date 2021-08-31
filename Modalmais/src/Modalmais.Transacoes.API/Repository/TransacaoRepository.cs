using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.Models;

namespace Modalmais.Transacoes.API.Repository
{
    public class TransacaoRepository : Repository<Transacao>
    {

        public TransacaoRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {

        }

    }
}
