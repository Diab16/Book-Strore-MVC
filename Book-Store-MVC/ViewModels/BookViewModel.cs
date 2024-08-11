using Book_Store_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_MVC.ViewModels
{
    public class BookViewModel
    {

        public int Id { get; set; }
      
        public string Title { get; set; }
        [Required ]
        public string Description { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
        [Required]
        public float? Price { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Imagefile { get; set; }
        public string PublisherName { get; set; }

        public string AuthorName { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public List<BookOrder>? BookOrders { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public List<Category>? Categorylist { get; set; }
        public List<Author>?Authorlist { get; set; }
        public List<Publisher>? publisherlist { get; set; }


        public Category? Category { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
