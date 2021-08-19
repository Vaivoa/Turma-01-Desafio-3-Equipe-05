using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Models.ObjectValues
{
    public class Celular
    {
        public Celular(string ddd, string numero)
        {
            DDD = ddd;
            Numero = numero;
        }
        public string DDD { get; }
        public string Numero { get; }
    }
}
