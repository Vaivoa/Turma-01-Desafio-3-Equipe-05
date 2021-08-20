using Modalmais.Business.Utils;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modalmais.Business.Service;
using Modalmais.Business.Interfaces.Notificador;
using FluentValidation.Validators;
using System.ComponentModel.DataAnnotations;

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
            if (UtilsDigitosNumericos.SoNumeros(cpf)) 
            { AdicionarNotificacao("CPF deve ser somente digitos numericos."); return false; }
            if (CpfValidacao.Validar(cpf)) 
            { AdicionarNotificacao("CPF deve ser valido."); return false; }
            return await _clienteRepository.ChecarEntidadeExistente(nameof(Cliente.CPF), cpf);
        }
        public async Task<bool> ChecarPorEmailSeClienteExiste(string email)
        {
            if (EmailValidacao.EmailValido(email) || new EmailAddressAttribute().IsValid(email)) 
            { AdicionarNotificacao("Email deve ser valido."); return false; }
            return await _clienteRepository.ChecarEntidadeExistente(nameof(Cliente.Contato.Email), email);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}
