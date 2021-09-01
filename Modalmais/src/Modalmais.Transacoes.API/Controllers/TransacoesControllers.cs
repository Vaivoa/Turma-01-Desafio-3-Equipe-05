using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using Modalmais.Transacoes.API.Repository;
using System;
using System.Threading.Tasks;
using Modalmais.Transacoes.API.Refit;
using Refit;

namespace Modalmais.Transacoes.API.Controllers
{

    [Route("api/v1/transacoes")]
    public class TransacoesControllers : MainController
    {

        private readonly TransacaoRepository _transacaoRepository;
        //private readonly ApiDbContext _dbContext;

        public TransacoesControllers(IMapper mapper,
                                     INotificador notificador,
                                     TransacaoRepository transacaoRepository
                                       //ApiDbContext dbContext
                                       ) : base(mapper, notificador)
        {
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

            //trocar isso pelo retorno do refit trazendo a conta e os dados e montar uma conta
            //validar se a conta esta ativa, se a chave pix esta ativa pix esta ativo
            // para passar para a transação como nas linhas abaixo

            //if (SeContaNaoAtiva || SePixNaoAtivo) return ResponseBadRequest("A conta ou pix informado nao pode receber transacoes no momento.");
            //if (SeNaoAcharContaNoRefitComAChavePix) return ResponseNotFound("Chave pix não encontrada.");
             

            var contaClient = RestService.For<IContaService>("https://localhost:5001/api/v1");
            var conta = await contaClient.ObterConta(transacaoRequest.Chave, $"{(int)transacaoRequest.Tipo}");
            var contaCorrente = conta.data.contaCorrente;

            transacaoRequest.AtribuirConta("7705895726698413"); // numero da conta que o refit trazer

            var transacao = _mapper.Map<Transacao>(transacaoRequest);

            var limiteAtingido = transacao.LimiteAtingido(transacaoRequest.Valor
                                 + await ObterTotalValorDoDiaPorNumeroConta(transacaoRequest.ObterNumeroConta()));
            if (limiteAtingido) return ResponseBadRequest("Limite diário de 100 mil atingido.");


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

    }
}