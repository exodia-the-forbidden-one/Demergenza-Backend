using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Domain.Entities.Admin;

namespace Demergenza.Persistence.Repositories.AdminRepository
{
    public class AdminWriteRepository : WriteRepository<Admin>, IAdminWriteRepository
    {
        public AdminWriteRepository(DemergenzaDbContext dbContext) : base(dbContext)
        {

        }
    }
}