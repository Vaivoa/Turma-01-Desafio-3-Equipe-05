using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Repository
{
    public class TransacaoRepository : Repository<Transacao>
    {

        private readonly IDistributedCache _dbRedis;

        public TransacaoRepository(ApiDbContext apiDbContext,
                                   IDistributedCache dbRedis,
                                   IMapper mapper)
                                   : base(apiDbContext, mapper)
        {
            _dbRedis = dbRedis;
        }



        public async Task<string> ObterExtratoCacheado(ExtratoRequest extratoRequest)
        {
            return await _dbRedis.GetStringAsync(GerarChaveRedis(extratoRequest.Conta, extratoRequest.Periodo.DataInicio, extratoRequest.Periodo.DataFinal));
        }

        public async Task ArmazenarExtratoCacheado(Extrato extrato)
        {
            var chave = GerarChaveRedis(extrato.Conta, extrato.Periodo.DataInicio, extrato.Periodo.DataFinal);

            var extratoSerializado = JsonSerializer.Serialize(extrato);

            var horas = extrato.DataCriacao.Hour;
            var minutos = extrato.DataCriacao.Minute;
            var segundos = extrato.DataCriacao.Second;
            var Expiracao = new TimeSpan(24, 0, 0) - new TimeSpan(horas, minutos, segundos);
            var opcoes = new DistributedCacheEntryOptions().SetSlidingExpiration(Expiracao);

            await _dbRedis.SetStringAsync(chave, extratoSerializado, opcoes);
        }

        public string GerarChaveRedis(string conta, DateTime dataInicio, DateTime dataFinal)
        {
            return $"Extrato:{conta}:Periodo:{dataInicio:dd-MM-yyyy}:{dataFinal:dd-MM-yyyy}";
        }

    }
}
