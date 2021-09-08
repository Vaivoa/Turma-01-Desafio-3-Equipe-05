using FluentValidation;
using FluentValidation.Validators;
using Modalmais.Core.Models.Enums;
using Modalmais.Core.Utils;
using System;

namespace Modalmais.API.DTOs.Validation
{
    public class ChavePixRequestValidator : AbstractValidator<ChavePixRequest>
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



        public ChavePixRequestValidator()
        {
            RuleFor(chavePixRequest => chavePixRequest.Tipo)
                .IsInEnum().WithMessage("Não é um tipo válido de PIX.")
                .NotNull().WithMessage("O tipo não pode ser branco ou nulo.");

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.CPF && chavePixRequest.Chave != null, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                .Length(ClienteCpfMinimoMaxChar, ClienteCpfMinimoMaxChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(CpfValidacao.Validar).WithMessage("O Cpf precisa ser um válido.")
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage("O Cpf precisa ser um válido.");
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Email && chavePixRequest.Chave != null, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                .Must(EmailValidacao.EmailValido).WithMessage("O Email informado é invalido.")
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("O Email informado é invalido.")
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteEmailMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                .WithMessage(ClientePropriedadeCharLimite);
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Telefone && chavePixRequest.Chave != null, () =>
            {

                RuleFor(chavePixRequest => chavePixRequest.Chave.Length != 11 ? "" : chavePixRequest.Chave.Substring(0, 2))
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(o =>
                {
                    int number;
                    if (!int.TryParse(o, out number))
                        return false;

                    return Enum.IsDefined(typeof(DDDBrasil), int.Parse(o));
                }
                ).WithMessage("Não è um DDD válido.");

                RuleFor(chavePixRequest => chavePixRequest.Chave.Length != 11 ? "" : chavePixRequest.Chave.Substring(2, (chavePixRequest.Chave.Length - 2)))
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(9).WithMessage("O numero tem que ter 11 digitos.")
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage("Somente digitos nos numeros");
            });

        }

    }
}
