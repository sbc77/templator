using System.Linq;

namespace TemplatorEngine.Pdf
{
    public static class Utils
    {
        public static double GetGreaterThanZeroOrDefault(double defaultValue, params double[] values)
        {

            if (values == null || values.Length == 0)
            {
                return defaultValue;    
            }

            var result = values.FirstOrDefault(x => x > 0);

            return result > 0 ? result : defaultValue;
        }
    }
}