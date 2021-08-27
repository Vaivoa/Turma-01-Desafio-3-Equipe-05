using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Modalmais.Business.Models
{
    public class Entidade
    {


        //[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public Entidade()
        {
            DataCriacao = DateTime.Now;
        }
        public Entidade(string id)
        {
            Id = id;
            DataCriacao = DateTime.Now;
        }
    }
}
