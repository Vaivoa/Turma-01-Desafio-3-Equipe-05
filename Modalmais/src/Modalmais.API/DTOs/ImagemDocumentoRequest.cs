using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Modalmais.API.DTOs
{
    public class ImagemDocumentoRequest
    {
        [Required(ErrorMessage = "A imagem do documento é obrigatoria.")]
        [DataType(DataType.Upload)]
        public IFormFile ImagemDocumento { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Banco { get; set; }

        [Required]
        public string Agencia { get; set; }

        [Required]
        public string Numero { get; set; }
    }
}
