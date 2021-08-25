using FluentValidation;
using Modalmais.Business.Models.ObjectValues;
using System;

namespace Modalmais.Business.Models.Validation
{
    public class ImagemDocumentoValidator : AbstractValidator<ImagemDocumento>
    {

        public static string PropriedadeVaziaNula => "O campo {PropertyName} não pode ser vazio ou nulo.";
        public static string StatusValido => "O Status não é válido";
        public static string UrlInvalida => "A imagem não foi salva corretamente.";
        public static string ImagemInvalida => "A imagem não está no formato PNG.";
        public static string StatusInvalido => "O status da imagem não é permitido pelo sistema.";
        public static string DataInvalida => "A data de criação precisa ser válida.";
        public static string DataPassado => "A data de criação deve ser uma data no passado.";
        public static string DataValidacao => "A data de validação deve ser uma data no passado, porem mais atual que a data de criação.";


        public ImagemDocumentoValidator()
        {


            RuleFor(imagemDocumento => imagemDocumento.UrlImagem)
                .NotNull().WithMessage(PropriedadeVaziaNula)
                .NotEmpty().WithMessage(PropriedadeVaziaNula)
                .Must(o => o.Contains("https://i.ibb.co/")).WithMessage(UrlInvalida);

            RuleFor(imagemDocumento => imagemDocumento.NomeImagem)
                .NotNull().WithMessage(PropriedadeVaziaNula)
                .NotEmpty().WithMessage(PropriedadeVaziaNula)
                .Must(o => o.Contains(".png")).WithMessage(ImagemInvalida);

            RuleFor(imagemDocumento => imagemDocumento.Status)
                .NotEmpty().WithMessage(PropriedadeVaziaNula)
                .IsInEnum().WithMessage(StatusInvalido);

            RuleFor(imagemDocumento => imagemDocumento.DataEnvio)
                .NotNull().WithMessage(PropriedadeVaziaNula)
                .NotEmpty().WithMessage(PropriedadeVaziaNula)
                .Must(date => date != default(DateTime)).WithMessage(DataInvalida)
                .LessThanOrEqualTo(p => DateTime.Now).WithMessage(DataPassado);

            RuleFor(imagemDocumento => imagemDocumento.DataValidacao)
                .NotNull().WithMessage(PropriedadeVaziaNula)
                .NotEmpty().WithMessage(PropriedadeVaziaNula)
                .Must(date => date != default(DateTime)).WithMessage(DataInvalida)
                .GreaterThanOrEqualTo(p => p.DataEnvio).WithMessage(DataValidacao);


        }
    }
}
