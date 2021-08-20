using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Models;
using Modalmais.Business.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteAdicionarRequest clienteRequest)
        {
            if (!ModelState.IsValid) return NotificarErrorsEmLista(ModelState);

            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (cliente.ListaDeErros.Any())
            {
                AdicionarListaValidacaoNotificacaoErro(cliente.ListaDeErros);
                return NotificarErrorsEmLista();
            }

            await _clienteServiceRequest.AdicionarCliente(cliente);

            if (ValidarEntidadeListaContemErros()) return NotificarErrorsEmLista();

            var clienteAdicionarResponse = _mapper.Map<ClienteAdicionarResponse>(cliente);


            return CreatedAtAction(nameof(ObterClientePeloId), new { clienteAdicionarResponse.Id },
                    new
                    {
                        sucess = true,
                        response = clienteAdicionarResponse
                    });

        }

        [HttpGet]
        public async Task<IActionResult> ListaClientes()
        {
            var ListaClientes = await _clienteServiceResponse.BuscarTodos();
            var ListaClientesResponse = new List<ClienteAdicionarResponse>();

            foreach (var cliente in ListaClientes)
            {
                ListaClientesResponse.Add(_mapper.Map<ClienteAdicionarResponse>(cliente));
            }

            return Ok(ListaClientesResponse);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePeloId(string id)
        {

            if (!ObjectIdValidacao.Validar(id))
            {
                AdicionarNotificacaoErro("Formato invalido");
                return NotificarErrorsEmLista();
            };

            var Cliente = await _clienteServiceResponse.BuscarClientePorId(id);
            
            if (Cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            var ClienteResponse = _mapper.Map<ClienteAdicionarResponse>(Cliente);


            return new OkObjectResult(ClienteResponse);
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
