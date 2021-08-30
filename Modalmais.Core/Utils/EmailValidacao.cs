using System.Text.RegularExpressions;

namespace Modalmais.Core.Utils
{
    public static class EmailValidacao
    {
        public static bool EmailValido(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
        + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
        + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);
            bool isvalid = regex.IsMatch(email);
            if (!isvalid) { return false; }
            return true;
        }

    }
}
