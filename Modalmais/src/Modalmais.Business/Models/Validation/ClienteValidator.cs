using FluentValidation;
using FluentValidation.Validators;
using mercadolivre.Business.Utils;

namespace Modalmais.Business.Models.Validation
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int ClienteCelularMinimoMaxChar => 9;
        public static string ClientePropriedadeCharLimite => "A quantidade de letras da propriedade {PropertyName} permitidas {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeVazia => "A {PropertyName} não pode ser vazio.";
        public static string ClientePropriedadeValida => "O {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 11 digitos numericos.";


        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
                .Length(ClienteNomeSobrenomeMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.Sobrenome)
                .Length(ClienteNomeSobrenomeMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.CPF)
                .Length(ClienteCpfMinimoMaxChar, ClienteCpfMinimoMaxChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(CpfValidacao.Validar).WithMessage(ClientePropriedadeValida)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);

            RuleFor(cliente => cliente.Contato.Email)
                .Must(EmailValidacao.EmailValido).WithMessage(ClientePropriedadeValida)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage(ClientePropriedadeValida)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteEmailMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite);

            RuleFor(cliente => cliente.Contato.Celular.DDD)
                .IsInEnum().WithMessage(ClientePropriedadeValida)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.Contato.Celular.Numero)
                .Length(ClienteCelularMinimoMaxChar, ClienteCelularMinimoMaxChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);
        }

    }
}
