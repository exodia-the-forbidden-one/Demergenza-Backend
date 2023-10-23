using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Persistence.Repositories.AdminRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Demergenza.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services){
            services.AddDbContext<DemergenzaDbContext>();
            services.AddScoped<IAdminReadRepository, AdminReadRepository>();
            services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
        }
    }
}