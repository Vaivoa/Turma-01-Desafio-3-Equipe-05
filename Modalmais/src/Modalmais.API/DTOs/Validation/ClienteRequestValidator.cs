﻿using FluentValidation;
using FluentValidation.Validators;
using Modalmais.API.DTOs;
using Modalmais.Core.Utils;

namespace Modalmais.Business.Models.Validation
{
    public class ClienteRequestValidator : AbstractValidator<ClienteAdicionarRequest>
    {
        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int CelularMinimoMaxDigitos => 9;
        public static string ClientePropriedadeCharLimite => "A quantidade de letras permitidas da propriedade {PropertyName} é de {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeVazia => "O campo {PropertyName} não pode ser vazio.";
        public static string ClientePropriedadeValida => "O campo {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O campo {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 11 digitos numericos.";


        public ClienteRequestValidator()
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

            RuleFor(cliente => cliente.Documento.CPF)
                .Length(ClienteCpfMinimoMaxChar, ClienteCpfMinimoMaxChar)
                .WithMessage(ClientePropriedadeCharLimite)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(CpfValidacao.Validar).WithMessage(ClientePropriedadeValida)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);

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
