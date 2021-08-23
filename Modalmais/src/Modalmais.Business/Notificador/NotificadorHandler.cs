using Modalmais.Business.Interfaces.Notificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Notificador
{
    //
    public class NotificadorHandler : INotificador
    {

        /*
         * Esta classe é um Handle da notificação ela quem irar conectar a bussines com Api 
         * Aqui não clasificamos os erros que irao aparece mas com algumas edições seria possivel
         * Aqui somente:
         * Guardamos as notificações em lista 
         * Verificamos se tem notificação na lista
         * E recuperamos a lista para apresentar os erros
         */
        private List<Notificacao> _notificacoes { get; set; }
        public NotificadorHandler()
        {
            //Iniciamos a lista _notificacoes
            _notificacoes = new();
        }
        public void AdicionarNotificacao(Notificacao notificacao)
        {
            //Add um objeto Notificacao a lista _notificacoes
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ListaNoticacoes()
        {
            //retorna a lista _notificacoes
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            //verifica se tem notificacoes
            return _notificacoes.Any();
        }
    }
}
