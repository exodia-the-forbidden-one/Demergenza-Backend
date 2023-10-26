

using Demergenza.Application.Helpers.Authentication;
using Demergenza.Application.Helpers.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demergenza.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ConfigurationHelper>();
        }
    }
}