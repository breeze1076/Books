using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task AddBookToClientAsync(int clientId, int bookId);
        Task DeleteBookFromClientAsync(int clientId, int bookId);
        bool ClientNameExists(Client client);
    }
}