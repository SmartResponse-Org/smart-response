using SmartResponse.Enums;
using SmartResponse.Implementation;
using SmartResponse.Interfaces;
using SmartResponse.Localization;
using System.Globalization;

namespace SmartResponse
{
    public class ResponseManager<T>
    {
        public static IResponseBuilder<T> Create(Culture culture = Culture.en)
        {
            var cultureInfo = new CultureInfo("en");

            switch (culture)
            {
                case Culture.ar:
                    cultureInfo = new CultureInfo("ar");
                    break;
            }

            return CreateHelper(cultureInfo);
        }

        public static IResponseBuilder<T> Create(string culture)
        {
            return CreateHelper(new CultureInfo(culture));
        }

        private static IResponseBuilder<T> CreateHelper(CultureInfo cultureInfo)
        {
            return new ResponseBuilder<T>(new Response<T>(),
                                          new CustomStringLocalizer<ErrorMessage>(cultureInfo),
                                          new CustomStringLocalizer<Label>(cultureInfo));
        }
    }
}
