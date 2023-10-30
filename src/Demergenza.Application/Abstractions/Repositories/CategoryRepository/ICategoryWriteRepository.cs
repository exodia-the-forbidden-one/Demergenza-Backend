using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demergenza.Domain.Entities.Menu;

namespace Demergenza.Application.Abstractions.Repositories.CategoryRepository
{
    public interface ICategoryWriteRepository :IWriteRepository<Category>
    {
        
    }
}