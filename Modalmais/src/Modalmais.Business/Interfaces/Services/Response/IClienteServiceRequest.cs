using Modalmais.Business.Models;
using System;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Response
{
    public interface IClienteServiceRequest : IDisposable
    {
        Task AdicionarCliente(Cliente clienteAdicionar);

        Task AdicionarImagemDocumentoCliente(Cliente clienteAdicionar);
    }
}
