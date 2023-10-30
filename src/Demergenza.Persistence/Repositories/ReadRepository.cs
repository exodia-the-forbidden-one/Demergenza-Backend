using System.Linq.Expressions;
using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demergenza.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    public DbSet<T> Table => _context.Set<T>();

    public ReadRepository(DemergenzaDbContext dbContext)
    {
        _context = dbContext;
    }

    public IQueryable<T> GetAll()
        => Table;

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        => Table.Where(expression);

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression)
        => await Table.FirstOrDefaultAsync(expression);
    
    public async Task<T?> GetByIdAsync(Guid id)
        => await Table.FirstOrDefaultAsync(t => t.Id == id);
}