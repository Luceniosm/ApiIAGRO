namespace IAGRO.Domain.Models;

public class BookSpecifications
{
    public DateTime OriginallyPublished { get; set; }
    public string Author { get; set; }
    public int PageCount { get; set; }
    public List<string> Illustrators { get; set; }
    public List<string> Genres { get; set; }
}