using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Day2.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private BookStoreContext context;
        private DbSet<ApplicationUser> users;
        public UserRepository(BookStoreContext _context) : base(_context)
        {
            context = _context;
            users = context.Set<ApplicationUser>();
        }
        public ApplicationUser GetByStringId(string id)
        {
            return users.Find(id);
        }
    }
}
