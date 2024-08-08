using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<BookOrder>? BookOrders { get; set; }
    }
}
