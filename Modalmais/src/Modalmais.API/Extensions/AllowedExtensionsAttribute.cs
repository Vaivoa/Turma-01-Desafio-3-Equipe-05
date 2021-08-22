using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Modalmais.API.Extensions
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public static string[] formato => new string[] { ".png" };
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null) return new ValidationResult(GetErrorMessageNull());


            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public static bool FormatoValido(object value)
        {
            var file = value as IFormFile;

            if (file == null) return false;


            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!formato.Contains(extension.ToLower()))
                    return false;
            }

            return true;
        }

        public string GetErrorMessage()
        {
            return $"Imagem apenas em PNG.";
        }

        public string GetErrorMessageNull()
        {
            return $"A imagem é obrigatoria, e deve ter menos de 4 MB e em PNG.";
        }
    }
}
