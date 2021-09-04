using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.DTOs.Validations;
using Modalmais.Transacoes.API.Models;
using Modalmais.Transacoes.API.Refit;
using Modalmais.Transacoes.API.Repository;
using Refit;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Controllers
{

    [Route("api/v1/transacoes")]
    public class TransacoesControllers : MainController
    {
        private readonly IContaService _contaService;
        private readonly TransacaoRepository _transacaoRepository;


        public TransacoesControllers(IContaService contaService,
                                    IMapper mapper,
                                     INotificador notificador,
                                      IDistributedCache dbRedis,
                                     TransacaoRepository transacaoRepository
                                       ) : base(mapper, notificador)
        {
            _contaService = contaService;
            _transacaoRepository = transacaoRepository;
        }


        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpPost("")]
        public async Task<IActionResult> RealizarTransacao(TransacaoRequest transacaoRequest)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);

            var conta = await ObterContaPelaChavePix(transacaoRequest);
            if (conta == null) return ResponseBadRequest("Não foi possível realizar a transação, tente novamente mais tarde.");
            if (conta.statusCode != 200) return ResponseNotFound("Chave pix não encontrada.");
            if (conta.data.contaCorrente.status != Status.Ativo || conta.data.contaCorrente.chavePix.ativo != Status.Ativo)
                return ResponseBadRequest("A conta ou a chave pix informada não pode receber transações no momento.");

            transacaoRequest.AtribuirConta(conta.data.contaCorrente.numero);

            var transacao = _mapper.Map<Transacao>(transacaoRequest);
            transacao.ConcluirTransacao();

            var limiteAtingido = transacao.LimiteAtingido(transacaoRequest.Valor
                                 + await ObterTotalValorDoDiaPorNumeroConta(transacaoRequest.ObterNumeroConta()));
            if (limiteAtingido) return ResponseBadRequest("Limite diário de 100 mil atingido.");
            if (transacao.EstaInvalido()) return ResponseEntidadeErro(transacao.ListaDeErros);

            _transacaoRepository.Add(transacao);
            if (!await _transacaoRepository.Salvar()) return ResponseInternalServerError("Erro na operação, tente mais tarde.");

            var transacaoResponse = _mapper.Map<TransacaoResponse>(transacao);

            return ResponseCreated(transacaoResponse);
        }


        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpGet("extratos/{agencia}/{conta}")]
        public async Task<IActionResult> ObterExtrato(string agencia, string conta, [FromQuery] DateTime? dataInicial, [FromQuery] DateTime? dataFinal)
        {

            var extratoRequest = new ExtratoRequest(agencia, conta);

            if (dataFinal == null && dataInicial != null || dataFinal != null && dataInicial == null)
                return ResponseBadRequest("O filtro do periodo de extrato precisa de 2 datas: dataInicial e dataFinal.");

            if (dataFinal != null && dataInicial != null)
                extratoRequest.AtribuirPeriodo((DateTime)dataFinal, (DateTime)dataInicial);

            var validar = new ExtratoRequestValidation().Validate(extratoRequest).Errors;
            if (validar.Any()) return ResponseEntidadeErro(validar);

            var extratoCache = await _transacaoRepository.ObterExtratoCacheado(extratoRequest);
            if (!String.IsNullOrEmpty(extratoCache))
            {
                var extratoSerializer = JsonSerializer.Deserialize<Extrato>(extratoCache);
                return ResponseOk(_mapper.Map<ExtratoResponse>(extratoSerializer));
            }

            if (!await _transacaoRepository.TransacoesDisponiveis(extratoRequest.Conta)) return ResponseNotFound("Não foi encontrado transações para esta conta.");

            var extrato = await _transacaoRepository.ObterPorContaDapper(extratoRequest);
            var extratoResponse = _mapper.Map<ExtratoResponse>(extrato);

            await _transacaoRepository.ArmazenarExtratoCacheado(extrato);

            return ResponseOk(extratoResponse);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<decimal> ObterTotalValorDoDiaPorNumeroConta(string numeroConta)
        {
            var transacoes = await _transacaoRepository.Buscar(c => c.Conta.Numero == numeroConta && c.DataCriacao.Date == DateTime.Today);
            decimal soma = 0;
            foreach (var transacao in transacoes) soma += transacao.Valor;
            return soma;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<RespostaConta> ObterContaPelaChavePix(TransacaoRequest transacaoRequest)
        {

            RespostaConta contaCorrente = null;
            try
            {
                var resultado = await _contaService.ObterConta(transacaoRequest.Chave, $"{(int)transacaoRequest.Tipo}");
                contaCorrente = resultado;
            }
            catch (ApiException ex)
            {
                var statusCode = ex.StatusCode;
                if (statusCode != HttpStatusCode.InternalServerError)
                    contaCorrente = ex.GetContentAsAsync<RespostaConta>().Result;
            }

            return contaCorrente;
        }
    }
}