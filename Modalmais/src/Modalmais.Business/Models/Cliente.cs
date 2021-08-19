using Modalmais.Business.Models.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Models
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public Contato Contato { get; set; }
        public ContaCorrente ContaCorrente { get; set; }
    }
}
