using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Domain.Entities.Menu;

namespace Demergenza.Persistence.Repositories.CategoryRepository
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(DemergenzaDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}