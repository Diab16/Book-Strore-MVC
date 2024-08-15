using Book_Store_MVC.Models;
using Book_Store_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Store_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookRepository bookRepository;
        private readonly CategoryRepository catRepository;


        public HomeController(ILogger<HomeController> logger  , BookRepository bookRepository, CategoryRepository catRepository)
        {
            _logger = logger;
            this.bookRepository = bookRepository;
             this.catRepository = catRepository;


        }


    public IActionResult Index()
        {
            List<Book> books = bookRepository.GetAll().Take(4).ToList();
            var categories = catRepository.GetAll();
            ViewBag.Category = categories;

            return View(books);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
