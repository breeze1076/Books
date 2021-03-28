using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _dbContext.Client.Include(a => a.Books).ToListAsync();
        }

        public async Task<Client> GetClientAsync(int id)
        {
            return await _dbContext.Client.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

        }

        public async Task CreateClientAsync(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(client)} must not be null");
            }
            await _dbContext.Client.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(client)} must not be null");
            }
            if (_dbContext.Client.Any(a => a.Id == client.Id))
            {
                _dbContext.Update(client);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _dbContext.Client.FindAsync(id);
            if (client != null)
            {
                _dbContext.Client.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddBookToClientAsync(int clientId, int bookId)
        {
            var client = await _dbContext.Client.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == clientId);
            var book = await _dbContext.Book.FindAsync(bookId);
            if (client != null && book != null)
            {
                client.Books.Add(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookFromClientAsync(int clientId, int bookId)
        {
            var client = await _dbContext.Client.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == clientId);
            var book = await _dbContext.Book.FindAsync(bookId);
            if (client != null && book != null)
            {
                client.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public bool ClientNameExists(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(client)} must not be null");
            }
            var existingClient = _dbContext.Client.FirstOrDefault(c => c.Name == client.Name && c.Id != client.Id);
            return existingClient != null;
        }
    }
}
