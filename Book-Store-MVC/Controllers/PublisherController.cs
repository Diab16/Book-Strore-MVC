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
            var publishers = _context.Publisher.Include(p => p.Books).ToList();
            return View(publishers);
        }
        public IActionResult Details(int id)
        {
            var publisher = _context.Publisher.Include(p => p.Books)
                            .FirstOrDefault(p => p.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

    }
}
