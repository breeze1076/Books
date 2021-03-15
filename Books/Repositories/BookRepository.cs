using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Data;
using Microsoft.EntityFrameworkCore;

namespace Books.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Book.ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _dbContext.Book.FindAsync(id);
        }

        public async Task CreateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(book)} must not be null");
            }
            await _dbContext.Book.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(book)} must not be null");
            }
            if (_dbContext.Book.Any(b => b.Id == book.Id))
            {
                _dbContext.Update(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _dbContext.Book.FindAsync(id);
            if (book != null)
            {
                _dbContext.Book.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
