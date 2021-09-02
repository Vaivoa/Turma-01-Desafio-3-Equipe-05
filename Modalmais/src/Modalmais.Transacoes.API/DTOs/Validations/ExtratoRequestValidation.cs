using FluentValidation;
using Modalmais.Core.Utils;

namespace Modalmais.Transacoes.API.DTOs.Validations
{
    public class ExtratoRequestValidation : AbstractValidator<ExtratoRequest>
    {
        public static readonly string CampoNaoPodeSerBrancoOuNulo = "O campo {PropertyName} não pode estar em branco ou ser nulo.";
        public static readonly string AgenciaInvalida = "As consultas de extratos só podem ser realizadas para a agencia 0001.";
        public static readonly string SomenteNumeros = "O campo {PropertyName} deve possuir somente numeros.";
        public static readonly string DataFinalInvalida = "O campo de filtro da data final deve ser maior que a data de inicio.";
        public static readonly string DataInicioInvalida = "O campo de filtro da data inicial deve ser menor que a data final.";



        public ExtratoRequestValidation()
        {
            RuleFor(extratoRequest => extratoRequest.Agencia)
               .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
               .Equal("0001").WithMessage(AgenciaInvalida);

            RuleFor(extratoRequest => extratoRequest.Conta)
               .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
               .Must(UtilsDigitosNumericos.SoNumeros).WithMessage(SomenteNumeros);

            RuleFor(extratoRequest => extratoRequest.Periodo.DataFinal)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .GreaterThan(extratoRequest => extratoRequest.Periodo.DataInicio).WithMessage(DataFinalInvalida);


            RuleFor(extratoRequest => extratoRequest.Periodo.DataInicio)
                .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
                .LessThan(extratoRequest => extratoRequest.Periodo.DataFinal).WithMessage(DataInicioInvalida);


            //RuleFor(extratoRequest => extratoRequest.Periodo.DataInicio)
            //    .NotEmpty().WithMessage(CampoNaoPodeSerBrancoOuNulo)
            //    .LessThanOrEqualTo(extratoRequest => (int)(extratoRequest.Periodo.DataFinal
            //                      - extratoRequest.Periodo.DataInicio).)
            //    .WithMessage(DataInicioInvalida);

        }

    }
}
