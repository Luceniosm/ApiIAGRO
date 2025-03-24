using IAGRO.Application.Extentions;
using IAGRO.Application.Interfaces;
using IAGRO.Domain.Dtos;
using IAGRO.Domain.Interfaces;
using IAGRO.Domain.Models;

namespace IAGRO.Application.Services;

public class BookAppService : AppService, IBookAppService
{
    private readonly IBookRepository _bookRepository;

    public BookAppService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public Task<Response<List<Book>>> GetBooksAsync(BookFilterRequest filter) =>
         Task.FromResult(Result(_bookRepository.GetByFilter(filter)));

    public Task<Response<decimal>> CalculateShipping(int idBook)
    {
        var book = _bookRepository.GetById(idBook);
        if (book == null)
            return Task.FromResult(Result<decimal>("Livro não encontrado"));

        var bookPrice = book.Price;
        var shippingRate = 0.2m;
        var shippingCost = bookPrice * shippingRate;
        
        return Task.FromResult(Result(shippingCost));
    }
    
}