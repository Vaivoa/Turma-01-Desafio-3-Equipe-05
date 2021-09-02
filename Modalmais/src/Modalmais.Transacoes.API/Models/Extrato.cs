using Modalmais.Transacoes.API.Models.ObjectValues;
using System.Collections.Generic;

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
        }

        protected Extrato()
        {
            //para realizar as migrations
        }

        public string Agencia { get; set; }
        public string Conta { get; set; }
        public Periodo Periodo { get; set; }
        public IEnumerable<Transacao> Transacoes { get; set; }


    }
}
