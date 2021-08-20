using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Models;
using Modalmais.Infra.Data;

namespace Modalmais.Infra.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {

        public ClienteRepository(DbContext context) : base(context)
        {

        }

    }
}
