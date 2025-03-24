using System.Collections.Generic;
using System.Threading.Tasks;
using IAGRO.Application.Services;
using IAGRO.Domain.Dtos;
using IAGRO.Domain.Interfaces;
using IAGRO.Domain.Models;
using Moq;
using Xunit;

namespace IAGRO.Test
{
    public class BookAppServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookAppService _bookAppService;

        public BookAppServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookAppService = new BookAppService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Arrange
            var filter = new BookFilterRequest(null, null, null, true);
            var books = new List<Book>
            {
                new Book{Id = 1, Name = "Journey to the Center of the Earth", Price = 10m}, 
                new Book{Id = 2, Name = "Harry Potter and the Goblet of Fire", Price = 20m},
            };
            _mockBookRepository.Setup(r => r.GetByFilter(filter)).Returns(books);

            // Act
            var result = await _bookAppService.GetBooksAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(books, result.Data);
            _mockBookRepository.Verify(r => r.GetByFilter(filter), Times.Once);
        }
        

        [Fact]
        public async Task GetBooksAsync_WithFilterNoMatch_ReturnsEmptyList()
        {
            // Arrange
            var filter = new BookFilterRequest("João", null, null, true);
            var emptyBookList = new List<Book>();
            _mockBookRepository.Setup(r => r.GetByFilter(filter)).Returns(emptyBookList);

            // Act
            var result = await _bookAppService.GetBooksAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Data);
            _mockBookRepository.Verify(r => r.GetByFilter(filter), Times.Once);
        }
        
        [Fact]
        public async Task GetBooksAsync_WithFilter_ReturnsListDesc()
        {
            // Arrange
            var filter = new BookFilterRequest(null, null, null, true);
            var books = new List<Book>
            {
                new Book{Id = 1, Name = "Journey to the Center of the Earth", Price = 10m}, 
                new Book{Id = 2, Name = "Harry Potter and the Goblet of Fire", Price = 20m},
            };
            _mockBookRepository.Setup(r => r.GetByFilter(filter)).Returns(books.OrderByDescending(el => el.Price).ToList());

            // Act
            var result = await _bookAppService.GetBooksAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(20m, result.Data.FirstOrDefault().Price);
            _mockBookRepository.Verify(r => r.GetByFilter(filter), Times.Once);
        }
        
        [Fact]
        public async Task GetBooksAsync_WithMultipleFilterCriteria_ReturnsFilteredBooks()
        {
            // Arrange
            var filter = new BookFilterRequest("Robert Kiyosaki", "Rich Dad, Poor Dad", "Personal Finance", false);
            var books = new List<Book>
            {
                new () { 
                    Id = 1, 
                    Name = "Harry Potter and the Philosopher's Stone", 
                    Price = 15m, 
                    Specifications = new BookSpecifications { 
                        OriginallyPublished = new DateTime(1997, 6, 26),
                        Author = "J.K. Rowling",
                        PageCount = 223,
                        Illustrators = ["Thomas Taylor"],
                        Genres = ["Fantasy", "Young Adult"]
                    }
                },
                new()
                { 
                    Id = 2, 
                    Name = "Harry Potter and the Chamber of Secrets", 
                    Price = 20m, 
                    Specifications = new BookSpecifications { 
                        OriginallyPublished = new DateTime(1998, 7, 2),
                        Author = "J.K. Rowling",
                        PageCount = 251,
                        Illustrators = ["Cliff Wright"],
                        Genres = ["Fantasy Fiction", "Drama", "Mystery"]
                    }
                },
                new ()
                { 
                    Id = 3, 
                    Name = "The Lord of the Rings", 
                    Price = 25m, 
                    Specifications = new BookSpecifications { 
                        OriginallyPublished = new DateTime(1954, 7, 29),
                        Author = "J.R.R. Tolkien",
                        PageCount = 1178,
                        Illustrators = ["Alan Lee"],
                        Genres = ["Fantasy Fiction", "Epic", "Mystery"]
                    }
                },
                new()
                { 
                    Id = 4, 
                    Name = "Rich Dad, Poor Dad", 
                    Price = 18m, 
                    Specifications = new BookSpecifications { 
                        OriginallyPublished = new DateTime(2017, 4, 11),
                        Author = "Robert Kiyosaki",
                        PageCount = 336,
                        Illustrators = [],
                        Genres = ["Personal Finance", "self-help"]
                    }
                }
            };
            
            _mockBookRepository.Setup(r => r.GetByFilter(filter)).Returns(books.Where(el => 
                el.Specifications.Genres.Contains(filter.Genre) &&
                el.Specifications.Author.Contains(filter.Author) &&
                el.Name.Contains(filter.NameBook)
                ).ToList());

            // Act
            var result = await _bookAppService.GetBooksAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Single(result.Data);
            Assert.Equal("Rich Dad, Poor Dad", result.Data[0].Name);
            Assert.Contains("Personal Finance", result.Data[0].Specifications.Genres);
            Assert.Contains("Robert Kiyosaki", result.Data[0].Specifications.Author);
            _mockBookRepository.Verify(r => r.GetByFilter(filter), Times.Once);
        }
        
        [Fact]
        public async Task CalculateShipping_BookWithZeroPrice_ReturnsZeroShipping()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Name = "Free Book", Price = 0m };
            _mockBookRepository.Setup(r => r.GetById(bookId)).Returns(book);

            // Act
            var result = await _bookAppService.CalculateShipping(bookId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(0m, result.Data);
            _mockBookRepository.Verify(r => r.GetById(bookId), Times.Once);
        }
        
        [Fact]
        public async Task CalculateShipping_WithValidBookId_ReturnsCorrectShippingCost()
        {
            // Arrange
            var bookId = 1;
            var bookPrice = 100m;
            var book = new Book { Id = bookId, Price = bookPrice };
            _mockBookRepository.Setup(r => r.GetById(bookId)).Returns(book);

            // Act
            var result = await _bookAppService.CalculateShipping(bookId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(20m, result.Data);
            _mockBookRepository.Verify(r => r.GetById(bookId), Times.Once);
        }
        
        [Fact]
        public async Task CalculateShipping_NonExistentBookId_ReturnsErrorMessage()
        {
            // Arrange
            int nonExistentBookId = 999;
            _mockBookRepository.Setup(r => r.GetById(nonExistentBookId)).Returns((Book)null);

            // Act
            var result = await _bookAppService.CalculateShipping(nonExistentBookId);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.Erros);
            Assert.Equal("Livro não encontrado", result.Erros[0]);
            _mockBookRepository.Verify(r => r.GetById(nonExistentBookId), Times.Once);
        }
    }
}
