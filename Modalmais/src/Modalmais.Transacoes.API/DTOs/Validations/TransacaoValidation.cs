using FluentValidation;
using FluentValidation.Validators;
using Modalmais.Core.Models.Enums;
using Modalmais.Core.Utils;
using System;

namespace Modalmais.Transacoes.API.DTOs.Validations
{
    public class TransacaoRequestValidation : AbstractValidator<TransacaoRequest>
    {


        public static readonly string NaoEhUmaChavePixValida = "Não é um tipo válido de Chave PIX.";
        public static readonly string CampoNaoPodeSerBrancoOuNulo = "O campo {PropertyName} não pode estar em branco ou ser nulo.";
        public static readonly int CPFQuantidadeDeDigitos = 11;
        public static readonly int EmailQuantidadeDeDigitosMin = 7;
        public static readonly int EmailQuantidadeDeDigitosMax = 50;
        public static readonly string NumeroTotalDeCaracteresPermitidos = "O campo {PropertyName} deve possuir {MaxLength} caracteres/digitos.";
        public static readonly string NumeroDeCaracteresPermitidos = "O campo {PropertyName} deve possuir entre {MinLength} e {MaxLength} caracteres/digitos.";
        public static readonly string CPFInvalido = "O CPF precisa ser um válido.";
        public static readonly string EmailInvalido = "O Email informado precisa ser um válido.";
        public static readonly string DDDInvalido = "O DDD informado não é válido.";
        public static readonly string SomenteNumerosChave = "O campo Chave deve possuir somente numeros.";
        public static readonly string TelefoneNumeroTotalDigitos = "O campo Chave deve ser um telefone e possuir 11 digitos.";



        public TransacaoRequestValidation()
        {

            RuleFor(chavePixRequest => chavePixRequest.Tipo)
               .IsInEnum().WithMessage(NaoEhUmaChavePixValida)
               .NotNull().WithMessage(CampoNaoPodeSerBrancoOuNulo);

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.CPF && chavePixRequest.Chave != null, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                .Length(CPFQuantidadeDeDigitos).WithMessage(NumeroTotalDeCaracteresPermitidos)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .Must(CpfValidacao.Validar).WithMessage(CPFInvalido)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(CPFInvalido);
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Email && chavePixRequest.Chave != null, () =>
            {
                RuleFor(chavePixRequest => chavePixRequest.Chave)
                .Must(EmailValidacao.EmailValido).WithMessage(EmailInvalido)
                .NotNull().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage(EmailInvalido)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .Length(EmailQuantidadeDeDigitosMin, EmailQuantidadeDeDigitosMax).WithMessage(NumeroDeCaracteresPermitidos);
            });

            When(chavePixRequest => chavePixRequest.Tipo == TipoChavePix.Telefone && chavePixRequest.Chave != null, () =>
            {

                RuleFor(chavePixRequest => chavePixRequest.Chave.Length != 11 ? "" : chavePixRequest.Chave.Substring(0, 2))
                .NotNull().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .Must(o =>
                {
                    int number;
                    if (!int.TryParse(o, out number))
                        return false;

                    return Enum.IsDefined(typeof(DDDBrasil), int.Parse(o));
                }
                ).WithMessage(DDDInvalido);

                RuleFor(chavePixRequest => chavePixRequest.Chave.Length != 11 ? "" : chavePixRequest.Chave.Substring(2, (chavePixRequest.Chave.Length - 2)))
                .NotNull().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .Length(9).WithMessage(TelefoneNumeroTotalDigitos)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(SomenteNumerosChave);
            });

            RuleFor(valor => valor.Valor)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .LessThanOrEqualTo(5000);

            RuleFor(descricao => descricao.Descricao)
                .MaximumLength(30);
        }
    }
}
