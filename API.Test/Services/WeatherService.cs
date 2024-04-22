using API.Test.Models;
using API.Test.Validation;
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

        public async Task<IResponse<string>> GetWeather(UserDto userDto)
        {
            var response = ResponseManager<string>.Create(Culture.ar);

            var validation = await new UserValidator().ValidateAsync(userDto);

            if (!validation.IsValid)
                return response.Set(validation.Errors);

            return response.Build("Done");
        }
    }
}
