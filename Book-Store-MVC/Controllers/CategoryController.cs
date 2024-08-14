using AutoMapper;
using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_MVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly BookStoreContext bookStore;
        private readonly IMapper mapper;

        public CategoryController(BookStoreContext bookStore, IMapper mapper)
        {
            this.bookStore = bookStore;
            this.mapper = mapper;
        }


        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> categories = bookStore.Category.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
      
        public ActionResult Create(Category category)
        {
             if(ModelState.IsValid)
            {
                bookStore.Category.Add(category);
                bookStore.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(category);


        }

      
        public ActionResult Edit(int id)
        {
            var category = bookStore.Category.Find(id);
            return View(category);
        }

     
        [HttpPost]
        public ActionResult Edit( Category category)
        {
              if(ModelState.IsValid)
              {
                Category categorydb = bookStore.Category.Find(category.Id);
                categorydb.Name = category.Name;
                bookStore.Update(categorydb);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        public ActionResult Delete(int id)
        {
            var category = bookStore.Category.Find(id);
            return View(category);
        }

        [HttpPost]
       
        public ActionResult Delete(Category category)
        {

            if (ModelState.IsValid)
            {
                var categorydb = bookStore.Category.Find(category.Id);
                bookStore.Category.Remove(categorydb);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
