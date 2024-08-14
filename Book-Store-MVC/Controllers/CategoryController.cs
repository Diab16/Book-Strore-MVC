using AutoMapper;
using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_MVC.Controllers
{
    public class CategoryController : Controller
    {

       
        private readonly IGenericRepository<Category> genericCategorey;
        private readonly IMapper mapper;

        public CategoryController(IGenericRepository<Category> genericCategorey, IMapper mapper)
        {
           
            this.genericCategorey = genericCategorey;
            this.mapper = mapper;
        }


        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> categories = genericCategorey.GetAll().ToList();
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
                genericCategorey.Add(category);
                genericCategorey.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(category);


        }

      
        public ActionResult Edit(int id)
        {
            var category = genericCategorey.GetById(id);
            return View(category);
        }

     
        [HttpPost]
        public ActionResult Edit( Category category)
        {
              if(ModelState.IsValid)
              {
                Category categorydb =genericCategorey.GetById(category.Id);
                categorydb.Name = category.Name;
                genericCategorey.Update(categorydb);
                genericCategorey.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        public ActionResult Delete(int id)
        {
            var category = genericCategorey.GetById(id);
            return View(category);
        }

        [HttpPost]
       
        public ActionResult Delete(Category category)
        {

            if (ModelState.IsValid)
            {
                var categorydb = genericCategorey.GetById(category.Id);
                genericCategorey.Delete(categorydb);
                genericCategorey.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
