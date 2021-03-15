using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _dbContext.Author.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            return await _dbContext.Author.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

        }

        public async Task CreateAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(author)} must not be null");
            }
            await _dbContext.Author.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(author)} must not be null");
            }
            if (_dbContext.Author.Any(a => a.Id == author.Id))
            {
                _dbContext.Update(author);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _dbContext.Author.FindAsync(id);
            if (author != null)
            {
                _dbContext.Author.Remove(author);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddBookToAuthorAsync(int authorId, int bookId)
        {
            var author = await _dbContext.Author.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == authorId);
            var book = await _dbContext.Book.FindAsync(bookId);
            if (author != null && book != null)
            {
                author.Books.Add(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookFromAuthorAsync(int authorId, int bookId)
        {
            var author = await _dbContext.Author.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == authorId);
            var book = await _dbContext.Book.FindAsync(bookId);
            if (author != null && book != null)
            {
                author.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
