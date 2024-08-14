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

            // GET: Publisher
            public IActionResult Index()
            {
                return View(_context.Publisher.ToList());
            }

            // GET: Publisher/Details/5
            public IActionResult Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var publisher = _context.Publisher
                    .Include(p => p.Books)
                    .FirstOrDefault(m => m.Id == id);

                if (publisher == null)
                {
                    return NotFound();
                }

                return View(publisher);
            }

            // GET: Publisher/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Publisher/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Publisher publisher)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(publisher);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(publisher);
            }

            // GET: Publisher/Edit/5
            public IActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var publisher = _context.Publisher.Find(id);
                if (publisher == null)
                {
                    return NotFound();
                }
                return View(publisher);
            }

            // POST: Publisher/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, Publisher publisher)
            {
                if (id != publisher.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(publisher);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PublisherExists(publisher.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(publisher);
            }

            // GET: Publisher/Delete/5
            public IActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var publisher = _context.Publisher
                    .Include(p => p.Books)
                    .FirstOrDefault(m => m.Id == id);

                if (publisher == null)
                {
                    return NotFound();
                }

                return View(publisher);
            }

            // POST: Publisher/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var publisher = _context.Publisher.Find(id);
                _context.Publisher.Remove(publisher);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            private bool PublisherExists(int id)
            {
                return _context.Publisher.Any(e => e.Id == id);
            }
        }
    }

