using Modalmais.Business.Models;
using Modalmais.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Models
{
    public class Transacao : Entidade
    {
        public Status StatusTransacao { get; set; }
        public TipoChavePix Tipo { get; set; }
        public string Chave { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public enum Status
        {
            Concluido,
            NaoConcluido
        }

        public bool LimiteTransferencia()
        {
            //Validar se já foi transferido 100.000.000 ou mais para uma mesma conta corrente.
            return true;
        }

    }
}
