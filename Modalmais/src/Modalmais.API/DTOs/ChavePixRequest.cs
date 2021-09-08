using Modalmais.Core.Models.Enums;

namespace Modalmais.API.DTOs
{
    public class ChavePixRequest
    {
        public string? Chave { get; set; }
        public TipoChavePix Tipo { get; set; }

    }
}
