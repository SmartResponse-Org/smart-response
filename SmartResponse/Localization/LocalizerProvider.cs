using System.Globalization;

namespace SmartResponse.Localization
{
    public class LocalizerProvider<T> 
    {
        public static ICustomStringLocalizer<T> GetLocalizer(CultureInfo culture = null)
        {
            return new CustomStringLocalizer<T>(culture);
        }
    }
}
