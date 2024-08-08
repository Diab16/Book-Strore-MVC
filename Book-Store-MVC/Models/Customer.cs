namespace Book_Store_MVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
