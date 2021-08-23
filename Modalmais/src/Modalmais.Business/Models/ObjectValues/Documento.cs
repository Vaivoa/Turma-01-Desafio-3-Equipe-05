using Modalmais.Business.Models.Enums;
using System.Collections.Generic;

namespace Modalmais.Business.Models.ObjectValues
{
    public class Documento
    {
        public Documento(string CPF)
        {
            this.CPF = CPF;
            Status = Status.Inativo;
            Imagens = new List<ImagemDocumento>();
        }

        public string CPF { get; private set; }
        public List<ImagemDocumento>? Imagens { get; private set; }
        public Status Status { get; private set; }


        public void AtivarDocumento()
        {
            Status = Status.Ativo;

        }

        public void DesativarDocumento()
        {
            Status = Status.Desativado;

        }

    }
}
