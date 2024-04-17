using Microsoft.Extensions.DependencyInjection;
using SmartResponse.Implementation;
using SmartResponse.Interfaces;
using SmartResponse.Localization;

namespace SmartResponse.IOC
{
    public static class DI
    {
        public static IServiceCollection AddSmartResponse(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICustomStringLocalizer<>), typeof(CustomStringLocalizer<>));
            //services.AddTransient(typeof(IResponse<>), typeof(Response<>));

            return services;
        }
    }
}
