namespace Demergenza.Application.Abstractions.Repositories.UserRepository;

public interface IUserWriteRepository<T> : IWriteRepository<T> where T : class 
{
    
}