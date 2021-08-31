namespace Modalmais.Core.Notificador
{
    public class Notificacao
    {
        /*
         * Esta classe ela é um modelo de notificação que será transmitidio poderiamos adicionar mais coisas aqui
         * para futura validacões. Pense nessa classse como um mensagem simples
         */

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
        public string Mensagem { get; }
    }
}
