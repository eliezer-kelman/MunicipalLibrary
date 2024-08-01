using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalLibrary.DAL;
using MunicipalLibrary.Models;
using System.Diagnostics;

namespace MunicipalLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Libraries()
        {
            List<Libary> libraries = Data.Get.Libraries.ToList();

            return View(libraries);
        }

        public IActionResult CreateLibrary()
        {
            return View(new Libary());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateLibrary(Libary libary)
        {
            Data.Get.Libraries.Add(libary);
            Data.Get.SaveChanges();
            return RedirectToAction(nameof(Libraries));
        }

        public IActionResult CreateShelf(int id)
        {
            Libary? lib = Data.Get.Libraries.FirstOrDefault(lib => lib.Id == id);
            if(lib == null)
            {
                return NotFound();
            }
            Shelf shelf = new Shelf();
            shelf.Library = lib;
            lib.ShelfList.Add(shelf);
            return View(shelf);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateShelf(Shelf shelf)
        {
            Libary? libary = Data.Get.Libraries.FirstOrDefault(lib =>lib.Id == shelf.Library.Id);
            if (libary == null)
            {
                return NotFound();
            }
            shelf.Library = libary;
            shelf.Id = 0;
            Data.Get.Shelves.Add(shelf);
            Data.Get.SaveChanges();
            return RedirectToAction("Libraries");
        }

        public IActionResult Shelfs(int id)
        {
            Libary? libary = Data.Get.Libraries.Include(lib => lib.ShelfList).FirstOrDefault(library => library.Id == id);
            if (libary == null)
            {
                return NotFound();
            }
            List<Shelf> shelves = libary.ShelfList;
            return View(shelves);
        }

        public IActionResult Books(int id)
        {
            Shelf? shelf = Data.Get.Shelves.Include(shelf => shelf.BookList).FirstOrDefault(shelf => shelf.Id == id);
            if (shelf == null)
            { 
                return NotFound(); 
            }
            return View(shelf.BookList);
        }

        public IActionResult CreateBook(int id)
        {
            Shelf? shelf1 = Data.Get.Shelves.FirstOrDefault(shelf => shelf.Id == id);
            if (shelf1 == null)
            {
                return NotFound();
            }
            Book book = new Book();
            book.Shelf = shelf1;
            shelf1.BookList.Add(book);
            return View(book);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateBook(Book book)
        {
            Shelf? shelf = Data.Get.Shelves.FirstOrDefault(shelf=> shelf.Id == book.Shelf.Id);
            if (book == null)
            {
                return NotFound();
            }
            int num = (book.Shelf.HeightShelf) - (book.HeightBook);
            if (num > 10)
            {
                return View();
            }
            if (num < 0)
            {
                return View();
            }
            if (shelf == null) {
                return NotFound(); }
            book.Shelf = shelf;
            book.Id = 0;
            Data.Get.Books.Add(book);
            Data.Get.SaveChanges();
            return RedirectToAction("Libraries");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
