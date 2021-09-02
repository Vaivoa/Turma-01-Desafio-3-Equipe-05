using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using Modalmais.Transacoes.API.Refit;
using Modalmais.Transacoes.API.Repository;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Controllers
{

    [Route("api/v1/transacoes")]
    public class TransacoesControllers : MainController
    {
        private readonly IContaService _contaService;
        private readonly TransacaoRepository _transacaoRepository;
        //private readonly ApiDbContext _dbContext;

        public TransacoesControllers(IContaService contaService,
                                    IMapper mapper,
                                     INotificador notificador,
                                     TransacaoRepository transacaoRepository
                                       //ApiDbContext dbContext
                                       ) : base(mapper, notificador)
        {
            _contaService = contaService;
            _transacaoRepository = transacaoRepository;
            //_dbContext = dbContext;
        }


        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpPost("")]
        public async Task<IActionResult> RealizarTransacao(TransacaoRequest transacaoRequest)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);

            var conta = await ObterContaPelaChavePix(transacaoRequest);
            if (conta == null) return ResponseBadRequest("Não possível realizar a transação, tente novamente mais tarde.");
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
                //var contaClient = RestService.For<IContaService>("https://localhost:5001/api/v1");
                var resultado = await _contaService.ObterConta(transacaoRequest.Chave, $"{(int)transacaoRequest.Tipo}");
                //var conta = await contaClient.ObterConta(transacaoRequest.Chave, $"{(int)transacaoRequest.Tipo}");
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