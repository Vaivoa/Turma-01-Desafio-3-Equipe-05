using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using Modalmais.Transacoes.API.Repository;
using System.Threading.Tasks;

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



        [CustomResponse(StatusCodes.Status204NoContent)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpPost("")]
        public async Task<IActionResult> RealizarTransacao(TransacaoRequest transacaoRequest)
        {
            if (!ModelState.IsValid) return ResponseModelErro(ModelState);

            var transacao = _mapper.Map<Transacao>(transacaoRequest);
            _transacaoRepository.Add(transacao);

            if (!await _transacaoRepository.Salvar()) return ResponseInternalServerError("Erro na operação, tente mais tarde.");

            return ResponseNoContent();
        }


    }
}