using IAGRO.Domain.Dtos;
using IAGRO.Domain.Models;

namespace IAGRO.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    List<Book> GetByFilter(BookFilterRequest request);
}