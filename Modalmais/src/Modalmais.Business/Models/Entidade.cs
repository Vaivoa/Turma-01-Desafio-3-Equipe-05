using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Modalmais.Business.Models
{
    public class Entidade
    {

        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public Entidade()
        {
            DataCriacao = DateTime.Now;
        }
    }
}