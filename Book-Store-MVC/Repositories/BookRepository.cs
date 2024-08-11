using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
