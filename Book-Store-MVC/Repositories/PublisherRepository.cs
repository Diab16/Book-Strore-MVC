using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Publisher = Book_Store_MVC.Models.Publisher;

namespace Book_Store_MVC.Repositories
{
    public class PublisherRepository:GenericRepository<Publisher> , IPublisherRepository
    {
        private BookStoreContext context;
        private DbSet<Category> publishers;
        public PublisherRepository(BookStoreContext _context) : base(_context)
        {
            context = _context;
            publishers = context.Set<Category>();
        }
    }
}
