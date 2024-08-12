using Book_Store_MVC.Models;

namespace Book_Store_MVC.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<BookOrder>? BookOrders { get; set; }
    }
}
