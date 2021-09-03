using Modalmais.Business.Models;
using Modalmais.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Request
{
    public interface IClienteServiceResponse : IDisposable
    {
        Task<bool> ChecarPorCpfSeClienteExiste(string cpf);
        Task<bool> ChecarPorEmailSeClienteExiste(string email);
        Task<bool> ChecarPorEmailSeClienteExiste(string email, string id);
        Task<Cliente> BuscarClientePorId(string id);
        Task<IEnumerable<Cliente>> BuscarTodos();
        Task<Cliente> BuscarClientePelaChavePix(string chavePix, TipoChavePix tipoPix);

    }
}
