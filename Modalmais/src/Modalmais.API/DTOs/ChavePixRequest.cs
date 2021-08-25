using Modalmais.Business.Models.Enums;

namespace Modalmais.API.DTOs
{
    public class ChavePixRequest
    {
        public ChavePixRequest(string chave, TipoChavePix tipo)
        {
            Chave = tipo == TipoChavePix.Aleatoria ? null : chave;
            Tipo = tipo;
        }

        public string? Chave { get; set; }
        public TipoChavePix Tipo { get; set; }

    }
}
