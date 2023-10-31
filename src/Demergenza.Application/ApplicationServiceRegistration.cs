

using Demergenza.Application.Helpers.Authentication;
using Demergenza.Application.Helpers.Configuration;
using Demergenza.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demergenza.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ConfigurationHelper>();
            services.AddScoped<TokenHelper>();
            services.AddScoped<ImageService>();
        }
    }
}