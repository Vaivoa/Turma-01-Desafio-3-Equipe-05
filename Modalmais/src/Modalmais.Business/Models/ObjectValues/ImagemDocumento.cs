using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Modalmais.Business.Models.Validation;
using Modalmais.Core.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Business.Models.ObjectValues
{
    public class ImagemDocumento
    {
        public string UrlImagem { get; private set; }
        public string NomeImagem { get; private set; }
        public DateTime DataEnvio { get; private set; }
        public DateTime DataValidacao { get; private set; }
        public Status Status { get; private set; }

        public ImagemDocumento(IFormFile documentorecebido)
        {
            Status = Status.Inativo;
            NomeImagem = documentorecebido.FileName;
            DataImagemRecebida();
            DocumentoEstaValido(documentorecebido);
            ListaDeErros = new List<ValidationFailure>();
        }

        private void DataImagemRecebida()
        {
            DataEnvio = DateTime.Now;
        }

        private void DataImagemValidacao()
        {
            DataValidacao = DateTime.Now;
        }

        public void DesativarImagemDocumento()
        {
            Status = Status.Desativado;

        }

        public void AtivarImagemDocumento()
        {
            Status = Status.Ativo;

        }

        public void AtribuirUrl(string urlImagem)
        {
            UrlImagem = urlImagem;
        }

        public bool DocumentoEstaValido(IFormFile documentorecebido)
        {
            var validacao = new Random().Next(1, 3) % 2 == 0;

            if (validacao)
            {
                DataImagemValidacao();
                AtivarImagemDocumento();
            }

            return validacao;
        }

        [BsonIgnore]
        public List<ValidationFailure> ListaDeErros { get; private set; }

        public bool EstaInvalido()
        {
            ListaDeErros.Clear();

            ListaDeErros = new ImagemDocumentoValidator().Validate(this).Errors;

            return ListaDeErros.Any();
        }

    }


}
