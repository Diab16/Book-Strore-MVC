using Book_Store_MVC.Models;
using Book_Store_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Book_Store_MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookStoreContext bookStore;

        public AuthorController(BookStoreContext bookStore)
        {
            this.bookStore = bookStore;
        }
        //Index Action: will get the list of aiuthors and theire books 

        public ActionResult Index()
        {
            List<Author> authors = bookStore.Author.Include(a => a.Books).ToList();
            return View(authors);
        }
        //information about authors by his id , 
        public ActionResult Details(int id)
        {
            Author author = bookStore.Author.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound(); 
            }
            return View(author);
        }
        public ActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var authors = bookStore.Author
                    .Include(a => a.Books)
                    .Where(a => a.Name.Contains(searchTerm))
                    .ToList();

                return View("Index", authors); 
            }

            return RedirectToAction(nameof(Index)); 
        }
        //create new author(get)
        public ActionResult Create()
        {
            return View();
        }

        //save the author in database
        [HttpPost]

        [Authorize(Roles = "admin, author")]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                bookStore.Author.Add(author);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        //edit existed author
        [Authorize(Roles = "admin, author")]
        public ActionResult Edit(int id)
        {
            Author author = bookStore.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        //save author edit in database
        [HttpPost]
        [Authorize(Roles = "admin, author")]
        public ActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                Author authorDb = bookStore.Author.Find(author.Id);
                if (authorDb == null)
                {
                    return NotFound();
                }
                authorDb.Name = author.Name;
                bookStore.Update(authorDb);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        [Authorize(Roles = "admin, author")]
        public ActionResult Delete(int id)
        {
            Author author = bookStore.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Authorize(Roles = "admin, author")]
        public ActionResult Delete(Author author)
        {
            if (ModelState.IsValid)
            {
                Author authorDb = bookStore.Author.Find(author.Id);
                if (authorDb == null)
                {
                    return NotFound();
                }
                bookStore.Author.Remove(authorDb);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
