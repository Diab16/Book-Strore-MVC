using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_MVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime DatePublished { get; set; }
        public float Price { get; set; }
        public string AuthorName { get; set; }
        public string ImageUrl { get; set; }
        public string PublisherName { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public List<BookOrder>? BookOrders { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
