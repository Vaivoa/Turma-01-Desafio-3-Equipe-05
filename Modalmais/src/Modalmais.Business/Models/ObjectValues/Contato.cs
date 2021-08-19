namespace Modalmais.Business.Models.ObjectValues
{
    public class Contato
    {
        public Contato(Celular celular, string email)
        {
            Celular = celular;
            Email = email;
        }
        public Celular Celular { get; private set; }
        public string Email { get; private set; }
    }
}
