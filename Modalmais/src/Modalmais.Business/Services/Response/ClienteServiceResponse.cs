﻿using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Service;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Models.Enums;
using Modalmais.Core.Utils;
using MongoDB.Driver;
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
            return await _clienteRepository.ObterPorId(id);
        }



        public async Task<Cliente> BuscarClientePelaChavePix(string chavePix, TipoChavePix tipoPix)
        {
            var filter = Builders<Cliente>.Filter.And(
                Builders<Cliente>.Filter.Where(p => p.ContaCorrente.ChavePix.Chave == chavePix),
                Builders<Cliente>.Filter.Where(p => p.ContaCorrente.ChavePix.Tipo == tipoPix)
            );

            return await _clienteRepository.BuscarComFiltro(filter);
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

        public async Task<bool> ChecarPorEmailSeClienteExiste(string email, string id)
        {
            var filter = Builders<Cliente>.Filter.And(
                Builders<Cliente>.Filter.Where(p => p.Id != id),
                Builders<Cliente>.Filter.Where(p => p.Contato.Email == email)
            );

            return await _clienteRepository.ChecarEntidadeExistente(filter);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}
