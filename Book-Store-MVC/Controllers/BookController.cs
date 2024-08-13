using AutoMapper;
using Book_Store_MVC.FileUpload;
using Book_Store_MVC.Models;
using Book_Store_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Policy;

namespace Book_Store_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly BookStoreContext bookStore;
        private readonly IMapper mapper;

        public BookController(BookStoreContext bookStore , IMapper mapper )
        {
            this.bookStore = bookStore;
            this.mapper = mapper;
        }

        #region Index
        public ActionResult Index(int id, string Search)
        {
            if (id == 0 && Search == null)
            {
                List<Book> books = bookStore.Books.Include(b => b.Category).Include(b => b.Author).Include(b => b.Publisher).ToList();
                List<Category> categories = bookStore.Category.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                ViewBag.Category = categories;
                return View(books);
            }
            else if (id != 0 && Search == null)
            {
                List<Book> books = bookStore.Books.Where(c => c.CategoryId == id).Include(b => b.Category).ToList();
                List<Category> categories = bookStore.Category.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
                ViewBag.Category = categories;
                return View(books);
            }
            else
            {
                string lowerSearch = Search.ToLower();

                List<Book> books = bookStore.Books
                    .Where(c => c.Author.Name.ToLower().Contains(lowerSearch) ||
                                c.Publisher.Name.ToLower().Contains(lowerSearch) ||
                                c.Title.ToLower().Contains(lowerSearch))
                    .Include(b => b.Category)
                    .ToList(); List<Category> categories = bookStore.Category.Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();
                ViewBag.Category = categories;
                return View(books);

            }



        }
        #endregion

        #region Filter
        public ActionResult Filter(int id)
        {
            List<Book> books = bookStore.Books.Where(c => c.Id == id).ToList();


            return View(books);
        }
        #endregion

        #region Details
        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            Book book = bookStore.Books.Where(b => b.Id == id).Include(b => b.Category).FirstOrDefault();
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);

            return View(viewModel);
        }


        #endregion

        #region Create

        public ActionResult Create()
        {

            BookViewModel viewModel = new BookViewModel();

            viewModel.Categorylist = bookStore.Category.ToList();
            viewModel.Authorlist = bookStore.Author.ToList();
            viewModel.publisherlist = bookStore.Publisher.ToList();

            return View(viewModel);



        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookmaped)
        {
          

            if (ModelState.IsValid)
                {
                string ImageUrl;
                ImageUrl= bookmaped.ImageUrl = UploadFile.Upload(bookmaped.Imagefile, "Imges");
                //  book.PublisherName =  " Publisher";
                //  book.AuthorName =  " Author";
                //Book bookMapped = mapper.Map<BookViewModel, Book>(book);

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

                var author = bookStore.Author.Find(bookmaped.AuthorId);
                var publisher = bookStore.Publisher.Find(bookmaped.PublisherId);

                if (author != null)
                {
                    book.AuthorName = author.Name; // Assign the AuthorName
                }
                if (publisher != null)
                {
                    book.PublisherName = publisher.Name; // Assign the PublisherName
                }

                     bookStore.Add(book);
                    int result = bookStore.SaveChanges();
                    if (result > 0)
                    {
                        TempData["Message"] = "Trainee Added Succsuusfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
            bookmaped.Categorylist = bookStore.Category.ToList();
            bookmaped.publisherlist = bookStore.Publisher.ToList();
            bookmaped.Authorlist = bookStore.Author.ToList();

                return View(bookmaped);

         
            
        }


        #endregion


        #region    Edit
        public ActionResult Edit(int id)
        {
            Book book = bookStore.Books.Where(b => b.Id == id).FirstOrDefault();
            ModelState.Remove("Imagefile"); // Remove the validation for Imagefile if editing
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);
            viewModel.Categorylist = bookStore.Category.ToList();
            viewModel.Authorlist = bookStore.Author.ToList();
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
                var book = bookStore.Books.Find(bookmodel.Id);
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



                var author = bookStore.Author.Find(bookmodel.AuthorId);
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
                bookStore.Books.Update(book);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            bookmodel.Categorylist = bookStore.Category.ToList();
            bookmodel.publisherlist = bookStore.Publisher.ToList();
            bookmodel.Authorlist = bookStore.Author.ToList();

            return View(bookmodel);
        }
        #endregion


        #region Delete
        public ActionResult Delete(int id)
        {
            Book book = bookStore.Books.Where(b => b.Id == id).FirstOrDefault();
            BookViewModel viewModel = mapper.Map<Book, BookViewModel>(book);
       
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Delete( int id ,BookViewModel bookmodel)
        {

            if (id == bookmodel.Id)
            {
                Book book = mapper.Map<BookViewModel, Book>(bookmodel);
                bookStore.Remove(book);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(bookmodel);
        }
        #endregion

    }
}
