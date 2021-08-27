namespace Modalmais.API.DTOs
{
    public class ContaPixResponse
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ContaCorrenteResponse ContaCorrente { get; set; }
    }
}
