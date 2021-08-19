using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;

namespace Modalmais.API.DTOs
{
    public class ClienteResponse
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string CPF { get; private set; }
        public Contato Contato { get; private set; }
        public ContaCorrente ContaCorrente { get; private set; }
    }
}
