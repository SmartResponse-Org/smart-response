using SmartResponse.Enums;
using SmartResponse.Implementation;
using SmartResponse.Interfaces;

namespace SmartResponse.Managers
{
    public class ResponseManager<T>
    {
        public static IResponse<T> Create(Culture culture = Culture.en)
        {
            return new Response<T>(culture);
        }
    }
}
