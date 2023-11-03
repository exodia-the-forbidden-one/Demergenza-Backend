using Demergenza.Application.Abstractions.Repositories.MenuRepository;
using Demergenza.Domain.Entities.Menu;

namespace Demergenza.Persistence.Repositories.MenuRepository
{
    public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(DemergenzaDbContext dbContext) : base(dbContext)
        {

        }
    }
}