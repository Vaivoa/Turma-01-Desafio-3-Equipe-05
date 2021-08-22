using Microsoft.AspNetCore.Http;
using Modalmais.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Modalmais.API.DTOs
{
    public class ImagemDocumentoRequest
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".png" })]
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
