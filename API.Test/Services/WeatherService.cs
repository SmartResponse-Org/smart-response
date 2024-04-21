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

            if(count < 5)
            {
                response.Append(MessageCode.InvalidEmail)
                    .Append(MessageCode.InvalidFileSize)
                    .Append(new ErrorModel
                    {
                        Code = "Ad",
                        Message = "Invalid AD"
                    });

                return response.Build("Wrong");
            }

            return response.Build();
        }
    }
}
