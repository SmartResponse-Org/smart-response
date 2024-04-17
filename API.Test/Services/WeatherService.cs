using SmartResponse.Enums;
using SmartResponse.Interfaces;
using SmartResponse.Managers;
using SmartResponse.Models;

namespace API.Test.Services
{
    public class WeatherService
    {
        public WeatherService()
        {
        }

        public IResponse<string> GetWeather(int count)
        {
            var response = ResponseManager<string>.Create(Culture.en);

            if (count < 1)
            {
                response.Finish("Phone", MessageCode.InbetweenValue, "Phone", "2", "11");

                return response.Finish();
            }

            var weather = "23 C";

            return response.Finish(weather);
        }
    }
}
