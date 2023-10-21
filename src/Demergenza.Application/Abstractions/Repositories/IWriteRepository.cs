using System.Linq.Expressions;

namespace Demergenza.Application.Abstractions.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddAsync(List<T> entities);
    bool Remove(T entity);
    Task<bool> Remove(string id);
    bool Update(T entity);
    Task<int> SaveAsync();
}