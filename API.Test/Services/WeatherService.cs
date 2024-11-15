using API.Test.Models;
using API.Test.Validation;
using SmartResponse;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using SmartResponse.Models;

namespace API.Test.Services
{
    public class WeatherService
    {
        public WeatherService()
        {
        }

        public async Task<IResponse<bool>> GetWeather(UserDto userDto)
        {
            var response = ResponseManager<bool>.Create(Culture.ar);

            response.Append(MessageCode.InbetweenValue, nameof(userDto.Username), "Age", "18", "25");

            response.Append(MessageCode.InvalidMinLength, "Name", "Name", "10");

            return response.Build();
        }
    }
}
