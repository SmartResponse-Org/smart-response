using System.Globalization;

namespace SmartResponse.Localization
{
    public class LocalizerProvider<T> where T : class
    {
        private static CustomStringLocalizer<T> Localizer { get; set; }

        public static ICustomStringLocalizer<T> GetLocalizer(CultureInfo culture = null)
        {
            Localizer = Localizer ?? new CustomStringLocalizer<T>(culture);

            return Localizer;
        }
    }
}
