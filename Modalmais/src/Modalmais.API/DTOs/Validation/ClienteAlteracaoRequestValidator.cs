using FluentValidation;
using FluentValidation.Validators;
using Modalmais.Core.Utils;

namespace Modalmais.API.DTOs.Validation
{
    public class ClienteAlteracaoRequestValidator : AbstractValidator<ClienteAlteracaoRequest>
    {

        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int CelularMinimoMaxDigitos => 9;
        public static int ClienteContaNumeroMaximoChar => 16;
        public static string ClienteContaCorrenteAgencia => "0001";
        public static string ClienteContaCorrenteBanco => "746";
        public static string ClientePropriedadeCharTamanho => "O numero da conta precisa ter 16 digitos.";
        public static string ClientePropriedadeCharLimite => "A quantidade de letras da propriedade {PropertyName} permitidas {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeVazia => "A {PropertyName} não pode ser vazio.";
        public static string ClientePropriedadeValida => "O {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 2 digitos numericos.";
        public static string ClienteDataValida => "A data precisa ser válida.";
        public static string ClienteDataPresente => "A data deve ser a presente.";
        public static string ClienteDataFutura => "A data deve ser futura";


        public ClienteAlteracaoRequestValidator()
        {


            RuleFor(cliente => cliente.Nome)
               .NotNull().WithMessage(ClientePropriedadeVazia)
               .Length(ClienteNomeSobrenomeMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
               .WithMessage(ClientePropriedadeCharLimite)
               .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.Sobrenome)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteNomeSobrenomeMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.Contato.Email)
                .Must(EmailValidacao.EmailValido).WithMessage(ClientePropriedadeValida)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage(ClientePropriedadeValida)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteEmailMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite);

            RuleFor(cliente => cliente.Contato.Celular.DDD)
                .IsInEnum().WithMessage(ClientePropriedadeValida)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.Contato.Celular.Numero)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .Length(CelularMinimoMaxDigitos)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);


        }

    }
}
