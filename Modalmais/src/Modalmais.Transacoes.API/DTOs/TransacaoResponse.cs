using Modalmais.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.DTOs
{
    public class TransacaoResponse 
    { 
    public StatusTransacao StatusTransacao { get; private set; }
    public TipoChavePix Tipo { get; private set; }
    public string Chave { get; private set; }
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }
    
    }
}
