using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Books.Models;
using Books.Repositories;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _repository.GetAllBooksAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _repository.CreateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _repository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name")] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _repository.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _repository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
