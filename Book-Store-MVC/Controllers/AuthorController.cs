using Book_Store_MVC.Models;
﻿using AutoMapper;
using Book_Store_MVC.FileUpload;
using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Book_Store_MVC.Repositories;
using Book_Store_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Book_Store_MVC.Controllers
{
    public class AuthorController : Controller { 
        private readonly BookRepository bookRepository;
        private readonly CategoryRepository catgenericRepository;
        private readonly IGenericRepository<Author> aurhrepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<Models.Publisher> publisherrepo;

        private readonly BookStoreContext bookStore;

        public AuthorController(BookStoreContext bookStore, BookRepository bookRepository, CategoryRepository catgenericRepository, IGenericRepository<Author> aurhrepo, IMapper mapper,
     IGenericRepository<Models.Publisher> publisherrepo)
        {
            this.bookStore = bookStore;
            this.bookRepository = bookRepository;
            this.catgenericRepository = catgenericRepository;
            this.aurhrepo = aurhrepo;
            this.mapper = mapper;
            this.publisherrepo = publisherrepo;
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
        //search by author name or book name
        public ActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var authors = bookStore.Author
                    .Include(a => a.Books)
                    .Where(a => a.Name.Contains(searchTerm) ||
                                a.Books.Any(b => b.Title.Contains(searchTerm)))
                    .ToList();

                return View("Index", authors);
            }

            return RedirectToAction(nameof(Index));
        }
        //create new author(get)
        public ActionResult Create()
        {
            return View(new AuthorViewModel());
        }

        //save the author in database
        [HttpPost]
        [Authorize(Roles = "Admin, author")]
        public ActionResult Create(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    Name = authorViewModel.AuthorName,
                };

                bookStore.Author.Add(author);
                bookStore.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(authorViewModel);
        }



        [Authorize(Roles = "Admin, author")]
        public ActionResult AddBook(int authorId)
        {
            var author = bookStore.Author.FirstOrDefault(a => a.Id == authorId);
            if (author != null)
            {
                BookViewModel viewModel = new BookViewModel();
                viewModel.AuthorId = author.Id;
                viewModel.Author = author;
                viewModel.AuthorName = author.Name;
                viewModel.Authorlist = bookStore.Author.ToList();
                viewModel.Categorylist = catgenericRepository.GetAll().ToList();
                viewModel.Authorlist = aurhrepo.GetAll().ToList();
                viewModel.publisherlist = publisherrepo.GetAll().ToList();

                return View(viewModel);
            }
            return View();
            



        }
        //public ActionResult AddBook(int authorId)
        //{
        //    var author = bookStore.Author.Include(a => a.Books).FirstOrDefault(a => a.Id == authorId);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    var viewModel = new AuthorViewModel
        //    {
        //        AuthorId = author.Id,
        //        AuthorName = author.Name,
        //        Categorylist = bookStore.Category.ToList()
        //    };

        //    return View(viewModel);
        //}
        [HttpPost]
        public ActionResult AddBook(BookViewModel bookmaped)
        {


            if (ModelState.IsValid)
            {
                string ImageUrl;
                ImageUrl = bookmaped.ImageUrl = UploadFile.Upload(bookmaped.Imagefile, "Imges");
                var author = bookStore.Author.FirstOrDefault(a => a.Name == bookmaped.AuthorName);
                Book book = new Book
                {
                    Title = bookmaped.Title,
                    Description = bookmaped.Description,
                    Language = bookmaped.Language,
                    DatePublished = bookmaped.DatePublished,
                    Price = (float)bookmaped.Price,
                    CategoryId = bookmaped.CategoryId,
                    PublisherId = bookmaped.PublisherId,
                    AuthorId = author.Id,
                    ImageUrl = ImageUrl
                };

                
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

                bookRepository.Add(book);
                bookRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            bookmaped.Categorylist = catgenericRepository.GetAll().ToList();
            bookmaped.Authorlist = aurhrepo.GetAll().ToList();
            bookmaped.publisherlist = publisherrepo.GetAll().ToList();

            return View(bookmaped);



        }

        //[HttpPost]
        //[Authorize(Roles = "admin, author")]
        //public ActionResult AddBook(AuthorViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var author = bookStore.Author.Include(a => a.Books).FirstOrDefault(a => a.Id == viewModel.AuthorId);
        //        if (author == null)
        //        {
        //            return NotFound();
        //        }

        //        var newBook = new Book
        //        {
        //            Title = viewModel.Title,
        //            Description = viewModel.Description,
        //            Price = viewModel.Price,
        //            CategoryId = viewModel.CategoryId
        //        };

        //        author.Books.Add(newBook);
        //        bookStore.SaveChanges();

        //        return RedirectToAction("Details", new { id = author.Id });
        //    }

        //    viewModel.Categories = bookStore.Category.ToList(); // Reload categories if form submission fails
        //    return View(viewModel);
        //}

        //edit existed author
        [Authorize(Roles = "Admin, author")]
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
        [Authorize(Roles = "Admin, author")]
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
        [Authorize(Roles = "Admin, author")]
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
        [Authorize(Roles = "Admin, author")]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = bookStore.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            bookStore.Author.Remove(author);
            bookStore.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
