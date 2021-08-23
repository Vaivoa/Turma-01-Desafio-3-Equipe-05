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
            if (ChecarPorCpfSeClienteExiste(clienteAdicionar.Documento.CPF).Result)
            { AdicionarNotificacao("CPF Existente em nosso banco de dados."); return; }
            if (ChecarPorEmailSeClienteExiste(clienteAdicionar.Contato.Email).Result)
            { AdicionarNotificacao("Email Existente em nosso banco de dados."); return; }
            await _clienteRepository.Adicionar(clienteAdicionar);
        }

        public async Task AdicionarImagemDocumentoCliente(Cliente clienteAdicionar)
        {
            //if (String.IsNullOrEmpty(clienteAdicionar.Documento.UrlImagem)) 
            //{
            //    await _clienteRepository.Update(clienteAdicionar);
            //    return;
            //}

            //var campos = Builders<Cliente>.Update.Set(o => o.Documento.UrlImagem, clienteAdicionar.Documento.UrlImagem);
            //campos.AddToSet(o => o.Documento.Status, clienteAdicionar.Documento.UrlImagem);

            await _clienteRepository.Update(clienteAdicionar);
        }
    }
}
