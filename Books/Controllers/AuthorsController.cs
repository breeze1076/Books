using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Books.Repositories;

namespace Books.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _repository;

        public AuthorsController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repository.GetAllAuthorsAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _repository.CreateAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _repository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name")] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _repository.UpdateAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _repository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _repository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        public async Task<IActionResult> AddBook([FromServices] IBookRepository bookRepository, int id)
        {
            var author = await _repository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var books = await bookRepository.GetAllBooksAsync();
            var model = new Tuple<Author, IEnumerable<Book>>(author, books);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(int id, int bookId)
        {
            await _repository.AddBookToAuthorAsync(id, bookId);
            return RedirectToAction(nameof(Details), new { Id = id });
        }

        [Route("[controller]/[action]/{id}/{bookId}")]
        public async Task<IActionResult> DeleteBook([FromServices] IBookRepository bookRepository, int id, int bookId)
        {
            var author = await _repository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var book = await bookRepository.GetBookAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            var model = new Tuple<Author, Book>(author, book);
            return View(model);
        }

        [Route("[controller]/[action]/{id}/{bookId}")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteBook")]
        public async Task<IActionResult> DeleteBookConfirmed(int id, int bookId)
        {
            await _repository.DeleteBookFromAuthorAsync(id, bookId);
            return RedirectToAction(nameof(Details), new { Id = id });
        }
    }
}
