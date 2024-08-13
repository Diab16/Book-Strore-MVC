using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Book_Store_MVC.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private BookStoreContext context;
        private DbSet<Book> books;
        public BookRepository(BookStoreContext _context) : base(_context)
        {
            context = _context;
            books = context.Set<Book>();
        }

        public int Count()
        {
            return books.Count();
        }

        public IEnumerable<Book> GetAll(int categoryId = 0, string searchTerm = "", int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Book> result;

            if (id == 0 && searchTerm == null)
            {

                result = books.Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            }
            else if (id != 0 && searchTerm == null)
            {
                result = books.Where(b => id == b.CategoryId).Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
                searchTerm = searchTerm.ToLower();
                result = books.Where(b => b.Title.ToLower().Contains(searchTerm) || b.Author.Name.ToLower().Contains(searchTerm) || b.Publisher.Name.ToLower().Contains(searchTerm)).Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }


            return result;
        }


        //    public IQueryable<Book> GetAll(out int TotalPages, int id = 0, string searchTerm = null, int pageNumber = 1, int pageSize = 10)
        //    {
        //        IQueryable<Book> resultbeforpagination = books.Include(b => b.Author).Include(b => b.Publisher);

        //        if (id != 0 && searchTerm == null)
        //        {
        //            resultbeforpagination = books.Where(b => id == b.CategoryId).Include(b => b.Author).Include(b => b.Publisher);
        //        }
        //        else
        //        {
        //            searchTerm = searchTerm.ToLower();
        //            resultbeforpagination = books.Where(b => b.Title.ToLower().Contains(searchTerm) || b.Author.Name.ToLower().Contains(searchTerm) || b.Publisher.Name.ToLower().Contains(searchTerm)).Include(b => b.Author).Include(b => b.Publisher);
        //        }

        //        TotalPages = resultbeforpagination.Count();
        //        var result = resultbeforpagination.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        //        return result;
        //    }

        //} 
    }
    }
