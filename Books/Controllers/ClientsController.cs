using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Books.Repositories;

namespace Books.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _repository;

        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _repository.GetAllClientsAsync();
            return View(clients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            await _repository.CreateClientAsync(client);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _repository.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name")] Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            await _repository.UpdateClientAsync(client);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _repository.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await _repository.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        public async Task<IActionResult> AddBook([FromServices] IBookRepository bookRepository, int id)
        {
            var client = await _repository.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var books = await bookRepository.GetAllBooksAsync();
            var model = new Tuple<Client, IEnumerable<Book>>(client, books);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(int id, int bookId)
        {
            await _repository.AddBookToClientAsync(id, bookId);
            return RedirectToAction(nameof(Details), new { Id = id });
        }

        [Route("[controller]/[action]/{id}/{bookId}")]
        public async Task<IActionResult> DeleteBook([FromServices] IBookRepository bookRepository, int id, int bookId)
        {
            var client = await _repository.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var book = await bookRepository.GetBookAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            var model = new Tuple<Client, Book>(client, book);
            return View(model);
        }

        [Route("[controller]/[action]/{id}/{bookId}")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteBook")]
        public async Task<IActionResult> DeleteBookConfirmed(int id, int bookId)
        {
            await _repository.DeleteBookFromClientAsync(id, bookId);
            return RedirectToAction(nameof(Details), new { Id = id });
        }
    }
}
