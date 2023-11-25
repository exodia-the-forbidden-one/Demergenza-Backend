using Microsoft.Extensions.Configuration;

namespace Demergenza.Application.Helpers.Configuration
{
    public class ConfigurationHelper
    {

        private readonly IConfiguration _configuration;
        public ConfigurationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenKey
        {
            get
            {
                return _configuration["Token:SecurityKey"]!;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _configuration["ConnectionStrings:PostgreSQL"]!;
            }
        }

    }
}