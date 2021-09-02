namespace Modalmais.API.DTOs
{
    public class ClienteAlteracaoRequest
    {

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ContatoAddRequest Contato { get; set; }

    }
}
