using Book_Store_MVC.Models;

namespace Book_Store_MVC.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
