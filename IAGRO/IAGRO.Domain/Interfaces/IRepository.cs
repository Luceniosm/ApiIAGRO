namespace IAGRO.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(int id);
}