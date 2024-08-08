using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_MVC.Models
{
    public class BookOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        public Book? Book { get; set; }
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public Order? Order { get; set; }
    }
}
