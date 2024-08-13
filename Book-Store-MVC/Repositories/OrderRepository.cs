using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Book_Store_MVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Repositories
{
    public class OrderRepository :GenericRepository<Order>, IOrderRepository
    {
        private BookStoreContext context;
        private DbSet<Order> orders;
        public OrderRepository(BookStoreContext _context):base(_context)
        {
            context = _context;
            orders = context.Set<Order>();
        }
    }
}
