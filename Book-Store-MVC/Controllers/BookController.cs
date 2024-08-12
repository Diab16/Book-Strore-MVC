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
        private readonly IGenericRepository<Book> genericRepository;
        private readonly IGenericRepository<Category> catgenericRepository;
        private readonly IGenericRepository<Author> aurhrepo;

        private readonly BookStoreContext bookStore;
        private readonly IMapper mapper;
       

        //public BookController(BookStoreContext bookStore , IMapper mapper )
        //{
        //    this.bookStore = bookStore;
        //    this.mapper = mapper;
        //}


        public BookController(IGenericRepository<Book> genericRepository, IGenericRepository<Category> catgenericRepository, IGenericRepository<Author> aurhrepo ,  IMapper mapper ,
          BookStoreContext bookStore)
        {
            this.genericRepository = genericRepository;
            this.catgenericRepository = catgenericRepository;
            this.aurhrepo = aurhrepo;
            this.mapper = mapper;
         
            this.bookStore = bookStore;
        }


        #region Index
        public ActionResult Index(int id, string Search)
        {
            // var bookowithnoinclude = genericRepository.GetAll().AsQueryable();
            var books = genericRepository.GetAll(b => b.Category, b => b.Author, b => b.Publisher).ToList();

            List<Category> categories = catgenericRepository.GetAll().Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            if (id == 0 && Search == null)
            {
                //  List<Book> books = bookStore.Books.Include(b => b.Category).Include(b => b.Author).Include(b => b.Publisher).ToList();
             
                ViewBag.Category = categories;
                return View(books);
            }
            else if (id != 0 && Search == null)
            {
                 books = books.Where(b=>b.CategoryId == id).ToList();
                ViewBag.Category = categories;
                return View(books);
            }
            else
            {
                string lowerSearch = Search.ToLower();

                  books = books
                    .Where(c => c.Author.Name.ToLower().Contains(lowerSearch) ||
                                c.Publisher.Name.ToLower().Contains(lowerSearch) ||
                                c.Title.ToLower().Contains(lowerSearch)).ToList();    
                
                   ViewBag.Category = categories;
                   return View(books);

            }



        }
        #endregion

        #region Filter
        //public ActionResult Filter(int id)
        //{
        //    List<Book> books = genericRepository.GetAll().Where(c => c.Id == id).ToList();
        //    return View(books);
        //}
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
            viewModel.publisherlist = bookStore.Publisher.ToList();

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
                var publisher = bookStore.Publisher.Find(bookmaped.PublisherId);
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
            bookmaped.publisherlist = bookStore.Publisher.ToList();

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
            viewModel.publisherlist = bookStore.Publisher.ToList();
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
                var publisher = bookStore.Publisher.Find(bookmodel.PublisherId);

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
            bookmodel.publisherlist = bookStore.Publisher.ToList();

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
