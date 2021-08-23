using Modalmais.Business.Models.Enums;

namespace Modalmais.Business.Models.ObjectValues
{
    public class Celular
    {
        public Celular(DDDBrasil DDD, string numero)
        {
            this.DDD = DDD;
            Numero = numero;
        }

        public DDDBrasil DDD { get; private set; }
        public string Numero { get; private set; }
    }
}
