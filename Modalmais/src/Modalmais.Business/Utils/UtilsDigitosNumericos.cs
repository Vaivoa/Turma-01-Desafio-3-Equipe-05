namespace mercadolivre.Business.Model.Validation.External
{
    public class UtilsDigitosNumericos
    {
        public static bool SoNumeros(string valor)
        {
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
