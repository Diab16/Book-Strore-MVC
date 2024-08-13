using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Controllers
{
    public class PublisherController : Controller
    {
        private readonly BookStoreContext _context;

        public PublisherController(BookStoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
