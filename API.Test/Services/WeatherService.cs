using API.Test.Localization;
using SmartResponse.Interfaces;
using SmartResponse.Managers;

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

            return response.Finish<ErrorMessage, Label>("A-11", nameof(count), "Phone", "0", "11");
        }
    }
}
