using System;

namespace Modalmais.Transacoes.API.Models
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

    }
}
