using Demergenza.Application.Abstractions.Repositories.AboutUsRepository;
using Demergenza.Domain.Entities.PageContents;

namespace Demergenza.Persistence.Repositories.AboutUsRepository;

public class AboutUsReadRepository : ReadRepository<AboutUs>, IAboutUsReadRepository
{
    public AboutUsReadRepository(DemergenzaDbContext dbContext) : base(dbContext)
    {
    }
}