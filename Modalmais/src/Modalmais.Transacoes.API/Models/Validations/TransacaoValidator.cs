using FluentValidation;
using FluentValidation.Validators;
using Modalmais.Core.Models.Enums;
using Modalmais.Core.Utils;
using System;

namespace Modalmais.Transacoes.API.Models.Validations
{
    public class TransacaoValidator : AbstractValidator<Transacao>
    {
        public static int ClienteNomeSobrenomeEmailMaximoChar => 50;
        public static int ClienteNomeSobrenomeMinimoChar => 3;
        public static int ClienteEmailMinimoChar => 5;
        public static int ClienteCpfMinimoMaxChar => 11;
        public static int ClienteCelularMinimoMaxChar => 9;
        public static int ClienteContaNumeroMaximoChar => 16;
        public static string ClienteContaCorrenteAgencia => "0001";
        public static string ClienteContaCorrenteBanco => "746";
        public static string ClientePropriedadeCharLimite => "A quantidade de letras da propriedade {PropertyName} permitidas {MinLength} a {MaxLength}.";
        public static string ClientePropriedadeVazia => "A {PropertyName} não pode ser vazio.";
        public static string ClientePropriedadeValida => "O {PropertyName} deve ser valido segundo as normativas.";
        public static string ClientePropriedadeSoNumeros => "O {PropertyName} deve ser formado somente por digitos numericos.";
        public static string ClienteDDDEnumValido => "O DDD deve ser formado por 11 digitos numericos.";

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
        public static readonly string ValorMaximoDeTransacao = "O valor maximo para uma transação é de 5000.";


        public TransacaoValidator()
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

            RuleFor(valor => valor.Valor)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .LessThanOrEqualTo(5000).WithMessage(ValorMaximoDeTransacao);

            RuleFor(descricao => descricao.Descricao)
                .MaximumLength(30).WithMessage(NumeroTotalDeCaracteresPermitidos);


            RuleFor(transacao => transacao.Conta.Agencia)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
                .Must(ag => ag == ClienteContaCorrenteAgencia).WithMessage(ClientePropriedadeValida);

            RuleFor(transacao => transacao.Conta.Banco)
                .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(ClientePropriedadeSoNumeros)
                .Must(banco => banco == ClienteContaCorrenteBanco).WithMessage(ClientePropriedadeValida);

            RuleFor(transacao => transacao.Conta.Numero)
                .NotEmpty().WithMessage(ClientePropriedadeVazia)
                .Length(ClienteContaNumeroMaximoChar).WithMessage(ClientePropriedadeCharLimite);

        }

    }
}
