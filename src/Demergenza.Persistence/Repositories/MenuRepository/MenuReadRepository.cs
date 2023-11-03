using Demergenza.Application.Abstractions.Repositories.MenuRepository;
using Demergenza.Domain.Entities.Menu;

namespace Demergenza.Persistence.Repositories.MenuRepository
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(DemergenzaDbContext dbContext) : base(dbContext)
        {

        }
    }
}