﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.API.Extensions;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Models;
using Modalmais.Business.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("api/v1/clientes")]
    public class ClientesCorrenteController : MainController
    {


        protected readonly IClienteServiceRequest _clienteServiceRequest;
        protected readonly IClienteServiceResponse _clienteServiceResponse;

        public ClientesCorrenteController(IMapper mapper,
                                       INotificador notificador,
                                       IClienteServiceRequest clienteServiceRequest,
                                       IClienteServiceResponse clienteServiceResponse
                                       ) : base(mapper, notificador)
        {
            _clienteServiceRequest = clienteServiceRequest;
            _clienteServiceResponse = clienteServiceResponse;
        }


        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteAdicionarRequest clienteRequest)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);

            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (!cliente.IsValid()) ResponseEntidadeErro(cliente.ListaDeErros);

            await _clienteServiceRequest.AdicionarCliente(cliente);

            if (NotificadorContemErros()) return ResponseBadRequest();

            var clienteAdicionarResponse = _mapper.Map<ClienteAdicionarResponse>(cliente);

            return ResponseCreated(clienteAdicionarResponse);

        }


        [CustomResponse(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> ObterTodosClientes()
        {
            var ListaClientes = await _clienteServiceResponse.BuscarTodos();
            var ListaClientesResponse = new List<ClienteAdicionarResponse>();

            foreach (var cliente in ListaClientes)
            {
                ListaClientesResponse.Add(_mapper.Map<ClienteAdicionarResponse>(cliente));
            }

            return ResponseOk(ListaClientesResponse);
        }

        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePeloId(string id)
        {

            if (!ObjectIdValidacao.Validar(id)) return ResponseBadRequest("Formato de dado inválido.");

            var Cliente = await _clienteServiceResponse.BuscarClientePorId(id);

            if (Cliente == null) return ResponseNotFound("O cliente não foi encontrado.");

            var ClienteResponse = _mapper.Map<ClienteAdicionarResponse>(Cliente);

            return ResponseOk(ClienteResponse);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public bool ValidarDocumento(IFormFile documentorecebido)
        {

            var numero = new Random().Next(1, 3);

            return numero % 2 == 0 ? true : false;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public string SalvarDocumento(IFormFile documentorecebido)
        {

            ////armazena fake

            var nomenclaturaPadrao = "_" + Guid.NewGuid().ToString();
            var urlFake = $"https://i.ibb.co/{documentorecebido.FileName}{nomenclaturaPadrao}";

            return urlFake;
        }


    }

}
