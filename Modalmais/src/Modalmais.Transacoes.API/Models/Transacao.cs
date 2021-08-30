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
        public StatusTransacao StatusTransacao { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public string Chave { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }

        

        public bool LimiteTransferencia()
        {
            //Validar se já foi transferido 100.000.000 ou mais para uma mesma conta corrente.
            return true;
        }

    }
}
