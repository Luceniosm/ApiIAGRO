using IAGRO.Domain.DBO;
using IAGRO.Domain.Dtos;
using IAGRO.Domain.Interfaces;
using IAGRO.Domain.Models;

namespace IAGRO.Domain.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book>? _books;

    public BookRepository(BookContext context)
    {
        _books = context.Books;
    }
    public  Book? GetById(int id) =>
        _books!.FirstOrDefault(b => b.Id == id);

    public List<Book> GetByFilter(BookFilterRequest request)
    {
        var query = _books!.AsQueryable();
        if (!string.IsNullOrEmpty(request.Author))
        {
            query = query.Where(el => el.Specifications.Author.Contains(request.Author));
        }
        if (!string.IsNullOrEmpty(request.NameBook))
        {
            query = query.Where(b => b.Name.Contains(request.NameBook));
        }
        if (!string.IsNullOrEmpty(request.Genre))
        {
            query = query.Where(b => b.Specifications.Genres.Contains(request.Genre));
        }
        
        return request.Asc ? 
            query.OrderBy(el => el.Price).ToList() : 
            query.OrderByDescending(el => el.Price).ToList();
    }
}