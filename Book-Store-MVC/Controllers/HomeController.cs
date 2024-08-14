using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Store_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext bookStoreContext;

        public HomeController(ILogger<HomeController> logger  , BookStoreContext bookStoreContext)
        {
            _logger = logger;
            this.bookStoreContext = bookStoreContext;
        }
     
     
        public IActionResult Index()
        {
            List<Book> books = bookStoreContext.Books.Take(4).ToList();
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
