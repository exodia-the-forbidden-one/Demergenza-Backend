using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Domain.Entities.Admin;

namespace Demergenza.Persistence.Repositories.AdminRepository
{
    public class AdminReadRepository : ReadRepository<Admin>, IAdminReadRepository
    {
        public AdminReadRepository(DemergenzaDbContext dbContext) : base(dbContext)
        {

        }
    }
}