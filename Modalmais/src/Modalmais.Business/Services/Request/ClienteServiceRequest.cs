using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Models;
using Modalmais.Business.Services.Response;
using System.Threading.Tasks;

namespace Modalmais.Business.Services.Request
{
    public class ClienteServiceRequest : ClienteServiceResponse, IClienteServiceRequest
    {
        public ClienteServiceRequest(INotificador notificador, IClienteRepository clienteRepository) : base(notificador, clienteRepository)
        {
        }

        public async Task AdicionarCliente(Cliente clienteAdicionar)
        {
            if (ChecarPorCpfSeClienteExiste(clienteAdicionar.CPF).Result)
            { AdicionarNotificacao("CPF Existente em nosso banco de dados"); return; }
            await _clienteRepository.Adicionar(clienteAdicionar);
        }
    }
}
