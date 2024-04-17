using API.Test.Localization;
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
            var response = ResponseManager<string>.Create();

            return response.Finish<Label, ErrorMessage>("78");
        }
    }
}
