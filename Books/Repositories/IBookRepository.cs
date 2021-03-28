using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        bool BookNameExists(Book book);
    }
}
