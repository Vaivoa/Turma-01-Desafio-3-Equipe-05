using System;

namespace Modalmais.Transacoes.API.Models
{
    public abstract class Entidade
    {
        public Guid Id { get; init; }
        public DateTime DataCriacao { get; init; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }


    }
}
