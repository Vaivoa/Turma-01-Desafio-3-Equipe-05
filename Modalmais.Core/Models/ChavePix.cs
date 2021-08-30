using FluentValidation.Results;
using Modalmais.Core.Models.Enums;
using Modalmais.Business.Models.Validation;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Core.Models
{
    public class ChavePix
    {

        public Status Ativo { get; private set; }
        public string Chave { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public ChavePix(string chave, TipoChavePix tipo)
        {
            ListaDeErros = new List<ValidationFailure>();
            Ativo = Status.Inativo;
            Chave = tipo == TipoChavePix.Aleatoria && chave == null ? GerarChavePix() : chave;
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

        public string GerarChavePix()
        {
            var chavePix = "";
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz--------";

            for (int i = 0; i < 32; i++)
            {
                if (random.Next(1, 3) % 2 == 0)
                {
                    chavePix += random.Next(0, 10).ToString();
                }
                else
                {
                    chavePix += chars.Select(c => chars[random.Next(chars.Length)]).First();
                }
            }

            return chavePix;
        }

        [BsonIgnore]
        public List<ValidationFailure> ListaDeErros { get; private set; }

        // Retorna True se tiverem erros
        public bool EstaInvalido()
        {
            ListaDeErros.Clear();

            ListaDeErros = new ChavePixValidator().Validate(this).Errors;

            return ListaDeErros.Any();
        }
    }
}
