using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Models.ObjectValues
{
    public class Contato
    {
        public Contato(Celular celular, string email)
        {
            Celular = celular;
            Email = email;
        }
        public Celular Celular { get; }
        public string Email { get; }
    }
}
