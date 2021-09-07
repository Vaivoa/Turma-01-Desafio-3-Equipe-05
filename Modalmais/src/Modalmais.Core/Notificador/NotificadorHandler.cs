using Modalmais.Core.Interfaces.Notificador;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Core.Notificador
{

    public class NotificadorHandler : INotificador
    {

        private List<Notificacao> _notificacoes { get; set; }
        public NotificadorHandler()
        {
            _notificacoes = new();
        }
        public void AdicionarNotificacao(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ListaNoticacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
