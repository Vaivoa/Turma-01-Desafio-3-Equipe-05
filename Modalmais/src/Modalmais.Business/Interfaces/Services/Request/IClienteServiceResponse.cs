using Modalmais.Business.Models;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Request
{
    public interface IClienteServiceResponse
    {
        Task<Cliente> BuscarClientePorId(int id);

        Task<Cliente> BuscarTodos();
    }
}
