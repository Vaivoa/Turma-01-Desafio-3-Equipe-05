using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Modalmais.Business.Models
{
    public class Entidade
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public Entidade()
        {
            DataCriacao = DateTime.Now;
        }
    }
}