using Modalmais.Business.Models.Enums;

namespace Modalmais.Business.Models
{
    public class ChavePix
    {
        public Status Ativo { get; private set; }
        public string Valor { get; private set; }
        public TipoChavePix Tipo { get; private set; }

        public ChavePix(string valor, TipoChavePix tipo = TipoChavePix.Aleatoria)
        {

            Ativo = Status.Inativo;
            Valor = valor;
            Tipo = tipo;

        }

        public void AtivarChavePix()
        {
            Ativo = Status.Ativo;

        }

        public void DesativarChavePix()
        {
            Ativo = Status.Desativado;

        }
    }
}
