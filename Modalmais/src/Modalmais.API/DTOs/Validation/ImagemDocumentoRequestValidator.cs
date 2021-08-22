using FluentValidation;
using Modalmais.API.DTOs;
using Modalmais.API.Extensions;
using Modalmais.Business.Utils;

namespace Modalmais.Business.Models.Validation
{
    public class ImagemDocumentoRequestValidator : AbstractValidator<ImagemDocumentoRequest>
    {
        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int ClienteCelularMinimoMaxChar => 9;
        public static int ClienteContaNumeroMaximoChar => 16;
        public static string ClienteContaCorrenteAgencia => "0001";
        public static string ClienteContaCorrenteBanco => "746";
        public static string ClientePropriedadeCharTamanho => "O numero da conta precisa ter 16 digitos.";
        public static string ClientePropriedadeCharLimite => "A quantidade de letras da propriedade {PropertyName} permitidas {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeVazia => "A {PropertyName} não pode ser vazio.";
        public static string ClientePropriedadeValida => "O {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 11 digitos numericos.";


        public ImagemDocumentoRequestValidator()
        {
            RuleFor(ImagemDocumento => ImagemDocumento.ImagemDocumento)
               .NotNull().WithMessage(ClientePropriedadeVazia)
               .NotEmpty().WithMessage(ClientePropriedadeVazia)
               .Must(MaxFileSizeAttribute.TamanhoValido)
               .Must(AllowedExtensionsAttribute.FormatoValido);

            RuleFor(ImagemDocumento => ImagemDocumento.Agencia)
               .NotNull().WithMessage(ClientePropriedadeVazia)
               .NotEmpty().WithMessage(ClientePropriedadeVazia)
               .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
               .Must(ag => ag == ClienteContaCorrenteAgencia).WithMessage(ClientePropriedadeValida);

            RuleFor(ImagemDocumento => ImagemDocumento.Banco)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
                .Must(banco => banco == ClienteContaCorrenteBanco).WithMessage(ClientePropriedadeValida);

            RuleFor(ImagemDocumento => ImagemDocumento.Numero)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteContaNumeroMaximoChar).WithMessage(ClientePropriedadeCharLimite);

            RuleFor(cliente => cliente.CPF)
                .Length(ClienteCpfMinimoMaxChar, ClienteCpfMinimoMaxChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(CpfValidacao.Validar).WithMessage(ClientePropriedadeValida)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);
        }

    }
}
