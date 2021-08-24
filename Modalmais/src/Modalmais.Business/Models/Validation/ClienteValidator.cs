using FluentValidation;
using FluentValidation.Validators;
using Modalmais.Business.Utils;
using System;

namespace Modalmais.Business.Models.Validation
{
    public class ClienteValidator : AbstractValidator<Cliente>
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
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 2 digitos numericos.";


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

            RuleFor(cliente => cliente.Documento.CPF)
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

            RuleFor(cliente => cliente.ContaCorrente.Agencia)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
                .Must(ag => ag == ClienteContaCorrenteAgencia).WithMessage(ClientePropriedadeValida);

            RuleFor(cliente => cliente.ContaCorrente.Banco)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
                .Must(banco => banco == ClienteContaCorrenteBanco).WithMessage(ClientePropriedadeValida);

            RuleFor(cliente => cliente.ContaCorrente.Numero)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteContaNumeroMaximoChar).WithMessage(ClientePropriedadeCharLimite);

            RuleFor(cliente => cliente.ContaCorrente.Status)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .IsInEnum().WithMessage(ClientePropriedadeValida)
                .NotEmpty().WithMessage(ClientePropriedadeVazia);

            RuleFor(cliente => cliente.ContaCorrente.DataCriacao)
                .NotEmpty().WithMessage("O campo data de criação não pode estar em branco.")
                .NotNull().WithMessage("O campo data de criação não pode ser nulo.")
                .Must(date => date != default(DateTime)).WithMessage("A data de criação precisa ser válida.")
                .LessThanOrEqualTo(p => DateTime.Now).WithMessage("A data de criação deve ser a presente.");

            RuleFor(cliente => cliente.ContaCorrente.DataMudancaStatus)
                .NotEmpty().WithMessage("O campo data de mudança de status da conta não pode estar em branco.")
                .NotNull().WithMessage("O campo data de mudança de status da conta não pode ser nulo.")
                .Must(date => date != default(DateTime)).WithMessage("A data de mudança de status da conta precisa ser válida.")
                .GreaterThanOrEqualTo(p => p.DataCriacao).WithMessage("A data de mudança de status da conta deve ser uma data futura.");

        }

    }
}
