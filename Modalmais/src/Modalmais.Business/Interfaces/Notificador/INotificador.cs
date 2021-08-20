using Modalmais.Business.Notificador;
using System.Collections.Generic;

namespace Modalmais.Business.Interfaces.Notificador
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ListaNoticacoes();
        void AdicionarNotificacao(Notificacao notificacao);
    }
}
