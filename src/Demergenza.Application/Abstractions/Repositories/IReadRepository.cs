using System.Linq.Expressions;

namespace Demergenza.Application.Abstractions.Repositories;

public interface IReadRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
    Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(string id);
}