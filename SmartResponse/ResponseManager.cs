using SmartResponse.Enums;
using SmartResponse.Implementation;
using SmartResponse.Interfaces;

namespace SmartResponse
{
    public class ResponseManager<T>
    {
        public static IResponseBuilder<T> Create(Culture culture = Culture.en)
        {
            return new ResponseBuilder<T>(new Response<T>(), culture);
        }

        public static IResponseBuilder<T> Create(string culture)
        {
            return new ResponseBuilder<T>(new Response<T>(), culture);
        }
    }
}
