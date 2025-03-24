using System.Text.Json;
using IAGRO.Domain.Models;

namespace IAGRO.Domain.DBO;

public class BookContext
{
    public List<Book>? Books { get; private set; }
    public BookContext(string jsonFilePath)
    {
        LoadBooksFromJsonFile(jsonFilePath);
    }
    
    private void LoadBooksFromJsonFile(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        Books = JsonSerializer.Deserialize<List<Book>>(jsonString, options);

        if (Books == null || Books.Count == 0)
        {
            throw new InvalidOperationException("Não foi possível carregar os livros do arquivo JSON.");
        }
    }
}