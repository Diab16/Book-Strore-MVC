using Book_Store_MVC.Models;
using Book_Store_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();
            return View(orders);
        }

        public IActionResult OrderSummary(int orderId)
        {
            var order = _context.Orders.Include(o => o.BookOrders)
                .ThenInclude(bo => bo.Book)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }
            var orderSummaryViewModel = new OrderSummaryViewModel
            {
                BookOrders = order.BookOrders,
                TotalPrice = (decimal)order.BookOrders.Sum(bo => bo.Book.Price * bo.Quantity),
                OrderDate = order.OrderDate
            };

            return View(orderSummaryViewModel);
        }
    }
}