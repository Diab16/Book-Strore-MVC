using Book_Store_MVC.Models;

namespace Book_Store_MVC.ViewModels
{
    public class OrderSummaryViewModel
    {
        public List<BookOrder> BookOrders { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
