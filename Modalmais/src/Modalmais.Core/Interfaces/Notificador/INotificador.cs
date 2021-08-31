using Modalmais.Core.Notificador;
using System.Collections.Generic;

namespace Modalmais.Core.Interfaces.Notificador
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ListaNoticacoes();
        void AdicionarNotificacao(Notificacao notificacao);
    }
}

