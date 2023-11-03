using System.Diagnostics;
using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Domain.Entities;
using Microsoft.AspNetCore.Routing.Tree;
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
        try
        {
            EntityEntry<T> entry = await _context.AddAsync(entity);
            if (entry.State == EntityState.Added) await SaveAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        try
        {
            Table.Remove(entity);
            await SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        try
        {
            T? entity = await Table.FindAsync(id);
            if (entity is null) throw new Exception("Invalid id");
            return await RemoveAsync(entity);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            Table.Attach(entity);
            EntityEntry<T> entry = _context.Update(entity);
            await SaveAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

}