using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task AddBookToAuthorAsync(int authorId, int bookId);
        Task DeleteBookFromAuthorAsync(int authorId, int bookId);
        bool AuthorNameExists(Author client);
    }
}
