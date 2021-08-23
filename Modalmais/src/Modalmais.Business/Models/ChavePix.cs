using Modalmais.Business.Models.Enums;
using System;

namespace Modalmais.Business.Models
{
    public class ChavePix
    {

        public Status Ativo { get; set; }
        public string Chave { get; set; }
        public TipoChavePix Tipo { get; set; }
        public DateTime DataCriacao { get; set; }


        public ChavePix(string chave, TipoChavePix tipo = TipoChavePix.Aleatoria)
        {

            Ativo = Status.Inativo;
            Chave = chave;
            Tipo = tipo;
            DataCriacao = DateTime.Now;

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
