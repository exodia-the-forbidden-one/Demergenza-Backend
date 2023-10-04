using Microsoft.Extensions.DependencyInjection;

namespace Demergenza.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services){
            services.AddDbContext<DemergenzaDbContext>();
        }
    }
}