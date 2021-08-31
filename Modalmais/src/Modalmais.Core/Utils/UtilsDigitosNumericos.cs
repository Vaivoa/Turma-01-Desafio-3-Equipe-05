namespace Modalmais.Core.Utils
{
    public class UtilsDigitosNumericos
    {
        public static bool SoNumeros(string valor)
        {
            if (valor == null) return false;

            foreach (var c in valor)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
