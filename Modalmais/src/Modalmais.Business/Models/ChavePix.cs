using Modalmais.Business.Models.Enums;

namespace Modalmais.Business.Models
{
    public class ChavePix
    {
        public Status Ativo { get; private set; }
        public string Chave { get; private set; }
        public TipoChavePix Tipo { get; private set; }

        public ChavePix(string chave, TipoChavePix tipo = TipoChavePix.Aleatoria)
        {

            Ativo = Status.Inativo;
            Chave = chave;
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
