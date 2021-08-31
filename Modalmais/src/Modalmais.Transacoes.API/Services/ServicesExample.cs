using Modalmais.Transacoes.API.Repository;
using System;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Services
{
    public class ServicesExample
    {
        private TransacaoRepository _repository;
        public ServicesExample(TransacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> ObterTotalValorDoDiaPorChave(string chave)
        {
            var transacoes = await _repository.Buscar(c => c.Chave == chave && c.DataCriacao == DateTime.Today);
            decimal soma = 0;
            foreach (var transacao in transacoes) soma += transacao.Valor;
            return soma;
        }
    }
}
