namespace Modalmais.Transacoes.API.Models
{
    public class Conta
    {
        public Conta(string banco, string agencia, string numero)
        {
            Banco = banco;
            Agencia = agencia;
            Numero = numero;
        }

        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string Numero { get; private set; }

    }
}
