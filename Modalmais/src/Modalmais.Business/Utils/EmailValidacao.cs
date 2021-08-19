using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mercadolivre.Business.Model.Validation.External
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
