﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.API.DTOs.Validation;
using Modalmais.Business.Interfaces.Services.Request;
using Modalmais.Business.Interfaces.Services.Response;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Models;
using Modalmais.Core.Models.Enums;
using Modalmais.Core.Utils;
using Modalmais.Core.WebResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{

    [Route("api/v1/clientes")]
    public class ClientesController : MainController
    {


        protected readonly IClienteServiceRequest _clienteServiceRequest;
        protected readonly IClienteServiceResponse _clienteServiceResponse;
        protected readonly KafkaProducerHostedService _kafkaProducerHostedService;

        public ClientesController(IMapper mapper,
                                       INotificador notificador,
                                       IClienteServiceRequest clienteServiceRequest,
                                       IClienteServiceResponse clienteServiceResponse,
                                       KafkaProducerHostedService kafkaProducerHostedService
                                       ) : base(mapper, notificador)
        {
            _clienteServiceRequest = clienteServiceRequest;
            _clienteServiceResponse = clienteServiceResponse;
            _kafkaProducerHostedService = kafkaProducerHostedService;
        }


        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<IActionResult> AdicionarCliente(ClienteAdicionarRequest clienteRequest)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);

            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (cliente.EstaInvalido()) ResponseEntidadeErro(cliente.ListaDeErros);

            await _clienteServiceRequest.AdicionarCliente(cliente);

            if (NotificadorContemErros()) return ResponseBadRequest();

            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);

            return ResponseCreated(clienteResponse);

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

            if (cliente.ContaCorrente.Numero != imagemDocumentoRequest.Numero ||
                cliente.Documento.CPF != imagemDocumentoRequest.CPF) return ResponseForbidden();

            if (!await AtribuirDocumentoAoCliente(cliente, imagemDocumentoRequest.ImagemDocumento))
                return ResponseBadRequest("A imagem do documento não é válida.");

            await _clienteServiceRequest.AdicionarImagemDocumentoCliente(cliente);

            if (NotificadorContemErros()) return ResponseBadRequest();

            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);

            return ResponseCreated(clienteResponse);

        }



        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [CustomResponse(StatusCodes.Status403Forbidden)]
        [HttpPost("{id}/chavepix")]
        public async Task<IActionResult> AdicionaChavePix([FromBody] ChavePixRequest chavePixRequest, [FromRoute] string id)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);
            if (!ObjectIdValidacao.Validar(id)) return ResponseBadRequest("Formato de dado inválido.");
            var cliente = await _clienteServiceResponse.BuscarClientePorId(id);
            if (cliente == null) return ResponseNotFound("O cliente não foi encontrado.");
            if (chavePixRequest.Tipo == TipoChavePix.CPF && chavePixRequest.Chave != cliente.Documento.CPF)
                return ResponseBadRequest("A chave Pix só pode ser o CPF, caso for igual ao do Titular.");
            if (chavePixRequest.Tipo != TipoChavePix.Aleatoria && String.IsNullOrEmpty(chavePixRequest.Chave))
                return ResponseBadRequest($"O tipo de chave {chavePixRequest.Tipo} requer uma chave.");

            if (chavePixRequest.Tipo == TipoChavePix.Aleatoria) chavePixRequest.Chave = null;

            var chavePix = _mapper.Map<ChavePix>(chavePixRequest);
            if (chavePix.EstaInvalido()) return ResponseEntidadeErro(chavePix.ListaDeErros);
            await _clienteServiceRequest.AdicionarPixContaCliente(cliente, chavePix);
            if (NotificadorContemErros()) return ResponseBadRequest();
            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);
            return ResponseCreated(clienteResponse);
        }


        [CustomResponse(StatusCodes.Status204NoContent)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarCadastroCliente(ClienteAlteracaoRequest clienteAlteracaoRequest, [FromRoute] string id)
        {

            if (!ModelState.IsValid) return ResponseModelErro(ModelState);
            if (!ObjectIdValidacao.Validar(id)) return ResponseBadRequest("Formato de dado inválido.");
            var cliente = await _clienteServiceResponse.BuscarClientePorId(id);
            if (cliente == null) return ResponseNotFound("O cliente não foi encontrado.");

            cliente.AlterarCliente(clienteAlteracaoRequest.Nome, clienteAlteracaoRequest.Sobrenome, clienteAlteracaoRequest.Contato.Email, clienteAlteracaoRequest.Contato.Celular.DDD, clienteAlteracaoRequest.Contato.Celular.Numero);

            await _clienteServiceRequest.AtualizarDadosContaCliente(cliente);

            if (NotificadorContemErros()) return ResponseBadRequest();

            _kafkaProducerHostedService.SendToKafka(cliente);

            return ResponseNoContent();
        }


        [CustomResponse(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> ObterTodosClientes()
        {
            var listaClientes = await _clienteServiceResponse.BuscarTodos();
            var listaClientesResponse = new List<ClienteResponse>();

            foreach (var cliente in listaClientes)
            {
                listaClientesResponse.Add(_mapper.Map<ClienteResponse>(cliente));
            }

            return ResponseOk(listaClientesResponse);
        }

        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePeloId(string id)
        {

            if (!ObjectIdValidacao.Validar(id)) return ResponseBadRequest("Formato de dado inválido.");

            var cliente = await _clienteServiceResponse.BuscarClientePorId(id);

            if (cliente == null) return ResponseNotFound("O cliente não foi encontrado.");

            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);

            return ResponseOk(clienteResponse);
        }



        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpGet("contas/chavepix")]
        public async Task<IActionResult> ObterContaPelaChavePix([FromQuery] string chave, [FromQuery] string tipo)
        {
            if (String.IsNullOrEmpty(chave)) ResponseBadRequest("Informe uma chave válida.");
            if (!int.TryParse(tipo, out int number)) return ResponseBadRequest("Não é um tipo válido de PIX.");

            var chavePixRequest = new ChavePixRequest { Chave = chave, Tipo = (TipoChavePix)number };

            var validar = new ChavePixRequestValidator().Validate(chavePixRequest).Errors;
            if (validar.Any()) return ResponseEntidadeErro(validar);

            var cliente = await _clienteServiceResponse.BuscarClientePelaChavePix(chavePixRequest.Chave, chavePixRequest.Tipo);
            if (cliente == null) return ResponseNotFound("Não foi encontrada uma conta relacionada com essa chave pix.");

            var contaPixResponse = _mapper.Map<ContaPixResponse>(cliente);

            return ResponseOk(contaPixResponse);
        }


        //[ApiExplorerSettings(IgnoreApi = true)]
        //[NonAction]
        //public string ArmazenarImagemDocumentoCloud(IFormFile documentorecebido)
        //{
        //    ////armazena fake
        //    var nomenclaturaPadrao = "_" + Guid.NewGuid().ToString();
        //    var urlFake = $"https://i.ibb.co/{documentorecebido.FileName + nomenclaturaPadrao}.png";
        //    return urlFake;
        //}


        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<bool> AtribuirDocumentoAoCliente(Cliente cliente, IFormFile documentoRecebido)
        {

            var imagemDocumento = new ImagemDocumento(documentoRecebido);
            if (imagemDocumento.Status == Status.Inativo) return false;

            var urlImagemDocumento = await ArmazenarImagemDocumentoCloud(documentoRecebido);
            if (urlImagemDocumento.success == false) return false;
            imagemDocumento.AtribuirUrl(urlImagemDocumento.data.image.url);

            if (imagemDocumento.EstaInvalido())
            {
                AdicionarNotificacaoErro(imagemDocumento.ListaDeErros);
                return false;
            }

            if (cliente.Documento.Imagens.Any())
            {
                foreach (var documento in cliente.Documento.Imagens)
                {
                    documento.DesativarImagemDocumento();
                }

                cliente.Documento.Imagens.Add(imagemDocumento);
                return true;
            }

            cliente.ContaCorrente.AtivarConta();
            cliente.Documento.AtivarDocumento();
            cliente.Documento.Imagens.Add(imagemDocumento);

            return true;
        }



        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<Root> ArmazenarImagemDocumentoCloud(IFormFile documentorecebido)
        {
            var image64 = "";
            using (var ms = new MemoryStream())
            {
                documentorecebido.CopyTo(ms);
                var fileBytes = ms.ToArray();
                image64 = Convert.ToBase64String(fileBytes);
            }

            HttpClient httpClient = new HttpClient();
            var multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(image64), "image");
            multiForm.Add(new StringContent(documentorecebido.FileName + "_" + Guid.NewGuid().ToString()), "name");
            var url = "https://api.imgbb.com/1/upload?key=8da220510687693589617194b14c9d43";
            var response = await httpClient.PostAsync(url, multiForm);

            if (!response.IsSuccessStatusCode) return null;

            var reponseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<Root>(reponseString);

            return jsonResponse;
        }

    }

}
