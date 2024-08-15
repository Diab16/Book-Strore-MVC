using Book_Store_MVC.Models;

namespace Book_Store_MVC.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
