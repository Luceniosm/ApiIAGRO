namespace IAGRO.Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public BookSpecifications Specifications { get; set; }
}
