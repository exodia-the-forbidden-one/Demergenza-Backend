using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demergenza.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly DemergenzaDbContext _context;
    public DbSet<T> Table => _context.Set<T>();


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

    public async Task<bool> RemoveAsync(T entity)
    {
        var entry = Table.Remove(entity);
        await SaveAsync();
        return entry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        T entity = await Table.FindAsync(id);
        return await RemoveAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        Table.Attach(entity);
        EntityEntry<T> entry = _context.Update(entity);
        await SaveAsync();
        return entry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

}