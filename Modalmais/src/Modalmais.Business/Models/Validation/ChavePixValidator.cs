using FluentValidation;
using FluentValidation.Validators;

using Modalmais.Business.Models.Enums;
using Modalmais.Business.Utils;
using System;


namespace Modalmais.Business.Models.Validation
{
    public class ChavePixValidator : AbstractValidator<ChavePix>
    {

        public static string ClientePropriedadeVazia => "A {PropertyName} não pode ser vazio.";
        public static string ClienteStatusValido => "O Status não é válido";

        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int ClienteCelularMinimoMaxChar => 9;
        public static string ClientePropriedadeCharLimite => "A quantidade de letras da propriedade {PropertyName} permitidas {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeValida => "O {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 11 digitos numericos.";
        public static string ClienteEmail => "O {PropertyName} deve ser formado válido";

        public static string DataInvalida => "A data de criação precisa ser válida.";
        public static string DataPresente => "A data de criação deve ser a presente.";
        public static string DataFutura => "A data deve ser futura";



        public ChavePixValidator()
        {
            RuleFor(ativo => ativo.Ativo)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .IsInEnum().WithMessage(ClienteStatusValido);

            RuleFor(chavePixRequest => chavePixRequest.Tipo)
               .IsInEnum().WithMessage("Não é um tipo válido de PIX.")
               .NotNull().WithMessage("O tipo não pode ser branco ou nulo.");

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.CPF, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                    .Length(ClienteCpfMinimoMaxChar, ClienteCpfMinimoMaxChar)
                    .WithMessage(ClientePropriedadeCharLimite)
                    .NotNull().WithMessage(ClientePropriedadeVazia)
                    .NotEmpty().WithMessage(ClientePropriedadeVazia)
                    .Must(CpfValidacao.Validar).WithMessage("O Cpf precisa ser um válido.")
                    .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros);
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Email, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                    .Must(EmailValidacao.EmailValido).WithMessage(ClienteEmail)
                    .NotNull().WithMessage(ClientePropriedadeVazia)
                    .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("O Email informado é invalido.")
                    .NotEmpty().WithMessage(ClientePropriedadeVazia)
                    .Length(ClienteEmailMinimoChar, ClienteNomeSobrenomeEmailMaximoChar)
                    .WithMessage(ClientePropriedadeCharLimite);
            });


            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Telefone, () =>
            {

                RuleFor(chavePixRequest => chavePixRequest.Chave.Substring(0, 2))
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


                RuleFor(chavePixRequest => chavePixRequest.Chave.Substring(2, (chavePixRequest.Chave.Length - 2)))
                    .NotNull().WithMessage(ClientePropriedadeVazia)
                    .NotEmpty().WithMessage(ClientePropriedadeVazia)
                    .Length(8, 9).WithMessage("O numero tem que ter entre 8 e 9 digitos.")
                    .Must(UtilsDigitosNumericos.SoNumeros).WithMessage("Somente digitos nos numeros");
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Aleatoria, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                    .NotNull().WithMessage(ClientePropriedadeVazia)
                    .NotEmpty().WithMessage(ClientePropriedadeVazia)
                    .Length(32).WithMessage("Falha ao gerar a chave aleatoria.");
            });


            RuleFor(chavePixRequest => chavePixRequest.DataCriacao)
                .NotNull().WithMessage(ClientePropriedadeVazia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(date => date != default(DateTime)).WithMessage(DataInvalida)
                .LessThanOrEqualTo(p => DateTime.Now).WithMessage(DataPresente);

        }

    }
}
