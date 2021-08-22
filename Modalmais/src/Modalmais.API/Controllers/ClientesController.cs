using AutoMapper;
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
        [HttpPost("")]
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


        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [CustomResponse(StatusCodes.Status403Forbidden)]
        [HttpPost("{id}/documentos")]
        public async Task<IActionResult> AdicionarImagemDocumento([FromForm] ImagemDocumentoRequest imagemDocumentoRequest, [FromRoute] string id)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);
            if (!ObjectIdValidacao.Validar(id)) return ResponseBadRequest("Formato de dado inválido.");

            var cliente = await _clienteServiceResponse.BuscarClientePorId(id);
            if (cliente == null) return ResponseNotFound("O cliente não foi encontrado.");

            if (cliente.ContaCorrente.Numero != imagemDocumentoRequest.Numero) return ResponseForbidden();

            if (!cliente.Documento.ImagemDocumentoValidar(imagemDocumentoRequest.ImagemDocumento)) return ResponseBadRequest("A imagem do documento não é válida.");

            AtribuirDocumentoAoCliente(cliente, imagemDocumentoRequest.ImagemDocumento);

            await _clienteServiceRequest.AdicionarImagemDocumentoCliente(cliente);

            if (NotificadorContemErros()) return ResponseBadRequest();

            var clienteAdicionarDocumentoResponse = _mapper.Map<ClienteAdicionarDocumentoResponse>(cliente);

            return ResponseCreated(clienteAdicionarDocumentoResponse);

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

            var ClienteResponse = _mapper.Map<ClienteResponse>(Cliente);

            return ResponseOk(ClienteResponse);
        }











        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public string ArmazenarImagemDocumentoCloud(IFormFile documentorecebido)
        {

            ////armazena fake

            var nomenclaturaPadrao = "_" + Guid.NewGuid().ToString();
            var urlFake = $"https://i.ibb.co/{documentorecebido.FileName}{nomenclaturaPadrao}.png";

            return urlFake;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public void AtribuirDocumentoAoCliente(Cliente cliente, IFormFile documentoRecebido)
        {

            var urlImagemDocumento = ArmazenarImagemDocumentoCloud(documentoRecebido);

            cliente.Documento.AtribuirImagemDocumento(urlImagemDocumento, documentoRecebido.FileName);

            cliente.ContaCorrente.AtivarConta();
        }

    }

}
