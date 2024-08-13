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
            IEnumerable<Book> result;
            if (categoryId == 0 && searchTerm == "")
            {
                result = books.Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (categoryId != 0 && searchTerm == "")
            {
                result = books.Where(b => categoryId == b.CategoryId).Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                searchTerm = searchTerm.ToLower();
                result = books.Where(b => b.Title.ToLower() == searchTerm || b.Author.Name.ToLower() == searchTerm || b.Publisher.Name.ToLower() == searchTerm).Include(b => b.Author).Include(b => b.Publisher).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return result;
        }
    }
}
