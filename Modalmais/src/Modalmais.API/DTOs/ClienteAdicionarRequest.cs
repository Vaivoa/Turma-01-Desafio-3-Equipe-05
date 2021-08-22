using Modalmais.Business.Models.Enums;

namespace Modalmais.API.DTOs
{
    public class ClienteAdicionarRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DocumentoAddRequest Documento { get; set; }
        public ContatoAddRequest Contato { get; set; }

    }

    public class ContatoAddRequest
    {
        public CelularAddRequest Celular { get; set; }
        public string Email { get; set; }

    }

    public class CelularAddRequest
    {
        public DDDBrasil DDD { get; set; }
        public string Numero { get; set; }

    }

    public class DocumentoAddRequest
    {
        public string CPF { get; set; }

    }
}
