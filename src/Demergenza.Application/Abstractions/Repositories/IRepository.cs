using Microsoft.EntityFrameworkCore;

namespace Demergenza.Application.Abstractions.Repositories;

public interface IRepository<T> where T :class
{
    public DbSet<T> Table { get; }
}