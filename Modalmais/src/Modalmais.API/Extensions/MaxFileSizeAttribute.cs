using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Modalmais.API.Extensions
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public static string MsgErro => "A imagem deve ter menos de 4 MB.";

        private static int tamanhoMaximo => 4 * 1024 * 1024;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null) return new ValidationResult(GetErrorMessageNull());

            if (file != null)
                if (file.Length > _maxFileSize) return new ValidationResult(GetErrorMessageLength());

            return ValidationResult.Success;
        }


        public static bool TamanhoValido(object value)
        {
            var file = value as IFormFile;

            if (file == null) return false;

            if (file != null) if (file.Length > tamanhoMaximo) return false;

            return true;
        }

        public string GetErrorMessageLength()
        {
            return $"O tamanho maximo da imagem do documento é de 4 MB.";
        }

        public string GetErrorMessageNull()
        {
            return $"A imagem é obrigatoria, e deve ter menos de 4 MB e ser PNG.";
        }
    }
}
