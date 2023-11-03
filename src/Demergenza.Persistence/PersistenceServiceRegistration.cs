using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Application.Abstractions.Repositories.MenuRepository;
using Demergenza.Persistence.Repositories.AdminRepository;
using Demergenza.Persistence.Repositories.CategoryRepository;
using Demergenza.Persistence.Repositories.MenuRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Demergenza.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DemergenzaDbContext>();
            services.AddScoped<IAdminReadRepository, AdminReadRepository>();
            services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
        }
    }
}