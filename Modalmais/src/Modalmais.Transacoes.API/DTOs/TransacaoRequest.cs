using Modalmais.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.DTOs
{
    public class TransacaoRequest
    {
        
        public TipoChavePix Tipo { get; set; }
        public string Chave { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

    }
}
