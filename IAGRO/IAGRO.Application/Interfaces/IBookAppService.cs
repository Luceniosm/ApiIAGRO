using IAGRO.Application.Extentions;
using IAGRO.Domain.Dtos;
using IAGRO.Domain.Models;

namespace IAGRO.Application.Interfaces;

public interface IBookAppService
{
    Task<Response<List<Book>>> GetBooksAsync(BookFilterRequest filter);
    Task<Response<decimal>> CalculateShipping(int idBook);
}