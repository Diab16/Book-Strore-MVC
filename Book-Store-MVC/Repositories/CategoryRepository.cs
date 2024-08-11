using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private BookStoreContext context;
        private DbSet<Category> categories;
        public CategoryRepository(BookStoreContext _context) : base(_context)
        {
            context = _context;
            categories= context.Set<Category>();
        }
    }
}
