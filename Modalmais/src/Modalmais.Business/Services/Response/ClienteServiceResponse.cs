using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Service;
using Modalmais.Business.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.Business.Services.Response
{
    public class ClienteServiceResponse : BaseService, IClienteServiceResponse
    {
        protected readonly IClienteRepository _clienteRepository;

        public ClienteServiceResponse(INotificador notificador, IClienteRepository clienteRepository) : base(notificador)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> BuscarClientePorId(string id)
        {
            //return await _Clienterepository.Buscar(nameof(Cliente.Id), id);
            return await _clienteRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            return await _clienteRepository.ObterTodos();
        }

        public async Task<bool> ChecarPorCpfSeClienteExiste(string cpf)
        {
            if (!UtilsDigitosNumericos.SoNumeros(cpf))
            { AdicionarNotificacao("CPF deve ser somente digitos numericos."); return false; }
            if (!CpfValidacao.Validar(cpf))
            { AdicionarNotificacao("CPF deve ser valido."); return false; }
            return await _clienteRepository.ChecarEntidadeExistente(nameof(Documento) + "." + nameof(Documento.CPF), cpf);
        }
        public async Task<bool> ChecarPorEmailSeClienteExiste(string email)
        {
            return await _clienteRepository.ChecarEntidadeExistente(nameof(Contato) + "." + nameof(Contato.Email), email);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}
