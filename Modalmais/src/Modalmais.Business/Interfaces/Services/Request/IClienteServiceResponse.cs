using Modalmais.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Request
{
    public interface IClienteServiceResponse : IDisposable
    {
        Task<bool> ChecarPorCpfSeClienteExiste(string cpf);
        Task<bool> ChecarPorEmailSeClienteExiste(string cpf);
        Task<Cliente> BuscarClientePorId(string id);
        Task<IEnumerable<Cliente>> BuscarTodos();
        Task<Cliente> BuscarClientePelaChavePix(string chavePix);

    }
}
