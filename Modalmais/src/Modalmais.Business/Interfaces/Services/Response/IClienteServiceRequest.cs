using Modalmais.Business.Models;
using Modalmais.Core.Models;
using System;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Response
{
    public interface IClienteServiceRequest : IDisposable
    {
        Task AdicionarCliente(Cliente clienteAdicionar);

        Task AdicionarImagemDocumentoCliente(Cliente clienteAdicionar);

        Task AdicionarPixContaCliente(Cliente clienteAdicionar, ChavePix chavePix);
    }
}
