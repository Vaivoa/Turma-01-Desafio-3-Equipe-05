using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
