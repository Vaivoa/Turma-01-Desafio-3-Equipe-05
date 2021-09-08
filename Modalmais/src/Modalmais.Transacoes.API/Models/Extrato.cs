using Modalmais.Transacoes.API.Models.ObjectValues;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Transacoes.API.Models
{
    public class Extrato : Entidade
    {
        public Extrato(string agencia, string conta, Periodo periodo, IEnumerable<Transacao> transacoes)
        {
            Agencia = agencia;
            Conta = conta;
            Periodo = periodo;
            Transacoes = transacoes;
            ValorMovimentado = ObterTotalValorMovimentadoDurantePeriodo();
        }

        private Extrato() { }

        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public Periodo Periodo { get; private set; }
        public decimal ValorMovimentado { get; private set; }
        public IEnumerable<Transacao> Transacoes { get; private set; }

        public void AtirbuirTrancacoes(IEnumerable<Transacao> transacoes) => Transacoes = transacoes;
        public decimal ObterTotalValorMovimentadoDurantePeriodo()
        {
            decimal valor = 0.0M;
            Transacoes.ToList().ForEach(o => valor += o.Valor);
            ValorMovimentado = valor;
            return valor;
        }


    }
}
