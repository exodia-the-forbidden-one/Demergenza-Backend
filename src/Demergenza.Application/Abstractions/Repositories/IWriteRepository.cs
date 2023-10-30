using System.Linq.Expressions;

namespace Demergenza.Application.Abstractions.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddAsync(List<T> entities);
    Task<bool> RemoveAsync(T entity);
    Task<bool> RemoveAsync(Guid id);
    Task<bool> UpdateAsync(T entity);
    Task<int> SaveAsync();
}