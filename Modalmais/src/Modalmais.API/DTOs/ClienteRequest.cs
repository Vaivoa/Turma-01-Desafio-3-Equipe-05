using Modalmais.Business.Models.ObjectValues;

namespace Modalmais.API.DTOs
{
    public class ClienteRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public Contato Contato { get; set; }

    }
}
