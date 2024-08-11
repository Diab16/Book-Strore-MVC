using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private BookStoreContext context;
        private DbSet<Author> authores;
        public AuthorRepository(BookStoreContext _context) : base(_context)
        {
            context = _context;
            authores = context.Set<Author>();
        }
    }
    
}

