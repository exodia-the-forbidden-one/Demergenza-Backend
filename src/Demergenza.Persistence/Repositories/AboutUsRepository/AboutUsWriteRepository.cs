using Demergenza.Application.Abstractions.Repositories.AboutUsRepository;
using Demergenza.Domain.Entities.PageContents;

namespace Demergenza.Persistence.Repositories.AboutUsRepository;

public class AboutUsWriteRepository : WriteRepository<AboutUs> , IAboutUsWriteRepository
{
    public AboutUsWriteRepository(DemergenzaDbContext dbContext) : base(dbContext)
    {
        
    }
}