using System;

namespace Modalmais.Transacoes.API.Models
{
    public abstract class Entidade
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }



        public Entidade()
        {
            Id = new Guid();
            DataCriacao = DateTime.Now;
        }
    }
}
