using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demergenza.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly DemergenzaDbContext _context;
    public DbSet<T> Table { get; }

    public WriteRepository(DemergenzaDbContext context)
    {
        _context = context;
    }


    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entry = await _context.AddAsync(entity);
        return entry.State == EntityState.Added;
    }

    public async Task<bool> AddAsync(List<T> entities)
    {
        await _context.AddRangeAsync();
        return true;
    }

    public bool Remove(T entity)
    {
        var entry = Table.Remove(entity);
        return entry.State == EntityState.Deleted;
    }

    public async Task<bool> Remove(string? id)
    {
        T entity = await Table.FirstOrDefaultAsync(entity => entity.Id == Guid.Parse(id));
        return Remove(entity);
    }

    public bool Update(T entity)
    {
        EntityEntry<T> entry = _context.Update(entity);
        return entry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}