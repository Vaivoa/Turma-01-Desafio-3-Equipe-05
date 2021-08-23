using Modalmais.Business.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Modalmais.API.DTOs
{
    public class ClienteResponse
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DocumentoResponse Documento { get; set; }
        public ContatoResponse Contato { get; set; }
        public ContaCorrenteResponse ContaCorrente { get; set; }

    }

    public class ContatoResponse
    {
        public CelularResponse Celular { get; set; }
        public string Email { get; set; }

    }

    public class CelularResponse
    {
        public DDDBrasil DDD { get; set; }
        public string Numero { get; set; }

    }

    public class ContaCorrenteResponse
    {
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public Status Status { get; set; }
        public DateTime DataCriacao { get; set; }

    }

    public class DocumentoResponse
    {
        public string CPF { get; set; }
        public Status Status { get; set; }
        public string UrlImagem { get; set; }
        public string NomeImagem { get; set; }
        public DateTime DataEnvio { get; set; }

    }

}