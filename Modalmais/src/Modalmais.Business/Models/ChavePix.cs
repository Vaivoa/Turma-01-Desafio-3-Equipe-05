using Modalmais.Business.Models.Enums;
using System;
using System.Linq;

namespace Modalmais.Business.Models
{
    public class ChavePix
    {

        public Status Ativo { get; private set; }
        public string Chave { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public ChavePix(string chave, TipoChavePix tipo)
        {

            Ativo = Status.Inativo;
            Chave = chave;
            Tipo = tipo;
            DataCriacao = DateTime.Now;

        }

        public ChavePix()
        {

            Ativo = Status.Inativo;
            Chave = GerarChavePix();
            Tipo = TipoChavePix.Aleatoria;
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

        public string GerarChavePix()
        {
            var chavePix = "";
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz-";

            for (int i = 0; i < 32; i++)
            {
                if (random.Next(1, 3) % 2 == 0)
                {
                    chavePix += random.Next(0, 10).ToString();
                }
                else
                {
                    chavePix += chars.Select(c => chars[random.Next(chars.Length)]);
                }
            }

            return chavePix;
        }
    }
}
