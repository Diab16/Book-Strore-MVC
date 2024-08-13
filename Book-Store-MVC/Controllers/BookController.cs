using AutoMapper;
using Book_Store_MVC.FileUpload;
using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Book_Store_MVC.Repositories;
using Book_Store_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Policy;

namespace Book_Store_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository  genericRepository;
        private readonly IGenericRepository<Category> catgenericRepository;
        private readonly IGenericRepository<Author> aurhrepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<Models.Publisher> publisherrepo;

        public BookController(BookRepository genericRepository, IGenericRepository<Category> catgenericRepository, IGenericRepository<Author> aurhrepo ,  IMapper mapper ,
         IGenericRepository<Models.Publisher> publisherrepo )
        {
            this.genericRepository = genericRepository;
            this.catgenericRepository = catgenericRepository;
            this.aurhrepo = aurhrepo;
            this.mapper = mapper;
            this.publisherrepo = publisherrepo;
        
        }


        #region Index
        public ActionResult Index(int id = 0, string searchTerm = null, int pageNumber = 1, int pageSize = 10)
        {
            List<Book> books = genericRepository.GetAll(id, searchTerm, pageNumber , pageSize).ToList(); 
            int totalbooks = books.Count;
            int totalpages = (int)Math.Ceiling((double)totalbooks / pageSize); ;
            List<Category> categories = catgenericRepository.GetAll().Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalpages;
            ViewBag.CategoryId = id;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.PageSize = pageSize;
            ViewBag.Category = categories;
            return View(books);

        }
        #endregion

      

        #region Details
        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            //  Book book = bookStore.Books.Where(b => b.Id == id).Include(b => b.Category).FirstOrDefault();
            Book book = genericRepository.GetById(id);
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);

            return View(viewModel);
        }


        #endregion

        #region Create

        public ActionResult Create()
        {

            BookViewModel viewModel = new BookViewModel();

            viewModel.Categorylist = catgenericRepository.GetAll().ToList();
            viewModel.Authorlist = aurhrepo.GetAll().ToList();
            viewModel.publisherlist = publisherrepo.GetAll().ToList();

            return View(viewModel);



        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookmaped)
        {
          

            if (ModelState.IsValid)
                {
                string ImageUrl;
                ImageUrl = bookmaped.ImageUrl = UploadFile.Upload(bookmaped.Imagefile, "Imges");

                Book book = new Book
                {
                    Title = bookmaped.Title,
                    Description = bookmaped.Description,
                    Language = bookmaped.Language,
                    DatePublished = bookmaped.DatePublished,
                    Price = (float)bookmaped.Price,
                    CategoryId = bookmaped.CategoryId,
                    PublisherId = bookmaped.PublisherId,
                    AuthorId = bookmaped.AuthorId,
                    ImageUrl = ImageUrl
                }; 

                var author = aurhrepo.GetById(bookmaped.AuthorId);
                var publisher = publisherrepo.GetById(bookmaped.PublisherId);
              //  var publisher = publisherrepo.GetById(bookmaped.PublisherId);

                if (author != null)
                {
                    book.AuthorName = author.Name; // Assign the AuthorName
                }
                if (publisher != null)
                {
                    book.PublisherName = publisher.Name; // Assign the PublisherName
                }

                     genericRepository.Add(book);
                     genericRepository.Save();
                    
                    return RedirectToAction(nameof(Index));
                }
            bookmaped.Categorylist = catgenericRepository.GetAll().ToList();
            bookmaped.Authorlist = aurhrepo.GetAll().ToList();
            bookmaped.publisherlist = publisherrepo.GetAll().ToList();

            return View(bookmaped);

         
            
        }


        #endregion


        #region    Edit
        public ActionResult Edit(int id)
        {
            Book book = genericRepository.GetById(id);
            ModelState.Remove("Imagefile"); // Remove the validation for Imagefile if editing
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);
            viewModel.Categorylist = catgenericRepository.GetAll().ToList();
            viewModel.Authorlist = aurhrepo.GetAll().ToList();
            viewModel.publisherlist = publisherrepo.GetAll().ToList();
            viewModel.ImageUrl = book.ImageUrl;
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit( BookViewModel bookmodel)
        {
            if (bookmodel.Id > 0) // Editing an existing book
            {
                ModelState.Remove("Imagefile"); // Remove the validation for Imagefile if editing
            }

            if (ModelState.IsValid)
            {
                var book  = genericRepository.GetById(bookmodel.Id);
                bookmodel.ImageUrl = UploadFile.Upload(bookmodel.Imagefile, "imges");
                book.Title = bookmodel.Title;
                book.Description = bookmodel.Description;
                book.Language = bookmodel.Language;
                book.DatePublished = bookmodel.DatePublished;
                book.Price = (float)bookmodel.Price;
                book.CategoryId = bookmodel.CategoryId;
                book.PublisherId = bookmodel.PublisherId;
                book.AuthorId = bookmodel.AuthorId;
                book.ImageUrl = bookmodel.ImageUrl; // Ensure ImageUrl is updated correctly




                var author = aurhrepo.GetById(bookmodel.AuthorId);
                var publisher = publisherrepo.GetById(bookmodel.PublisherId);

                if (author != null)
                {
                    book.AuthorName = author.Name; // Assign the AuthorName
                }
                if (publisher != null)
                {
                    book.PublisherName = publisher.Name; // Assign the PublisherName
                }
               // Book book = mapper.Map<BookViewModel, Book>(bookmodel);
                genericRepository.Update(book);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            bookmodel.Categorylist = catgenericRepository.GetAll().ToList();
            bookmodel.Authorlist = aurhrepo.GetAll().ToList();
            bookmodel.publisherlist = publisherrepo.GetAll().ToList();

            return View(bookmodel);
        }
        #endregion


        #region Delete
        public ActionResult Delete(int id)
        {
            var book = genericRepository.GetById(id);
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);
       
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Delete( int id ,BookViewModel bookmodel)
        {

            if (id == bookmodel.Id)
            {
                Book book = mapper.Map<BookViewModel, Book>(bookmodel);
                genericRepository.Delete(book);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(bookmodel);
        }
        #endregion



    }
}
