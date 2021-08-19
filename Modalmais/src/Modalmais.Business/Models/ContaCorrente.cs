using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Models
{
    public class ContaCorrente : Entidade
    {
        public string Codigo { get; }
        public string Agencia { get; }

        public ContaCorrente()
        {
            Codigo = "746";
            Agencia = "0001";
        }
    }
}
