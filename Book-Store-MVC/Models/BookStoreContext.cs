using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC.Models
{
    public class BookStoreContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookOrder> BookOrder { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George R.R. Martin" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" },
                new Author { Id = 4, Name = "Agatha Christie" },
                new Author { Id = 5, Name = "Stephen King" }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Bloomsbury Publishing" },
                new Publisher { Id = 2, Name = "Bantam Books" },
                new Publisher { Id = 3, Name = "HarperCollins" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fantasy" },
                new Category { Id = 2, Name = "Fiction" },
                new Category { Id = 3, Name = "Mystery" },
                new Category { Id = 4, Name = "Horror" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "Fantasy novel about a young wizard's journey.",
                    Language = "English",
                    DatePublished = new DateTime(1997, 6, 26),
                    Price = 19.99f,
                    PublisherId = 1,
                    PublisherName = "Bloomsbury Publishing",
                    ImageUrl = "url_to_image_1",
                    AuthorName = "J.K. Rowling",
                    CategoryId = 1, // Category: Fantasy
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "A Game of Thrones",
                    Description = "First book in the epic fantasy series A Song of Ice and Fire.",
                    Language = "English",
                    DatePublished = new DateTime(1996, 8, 6),
                    Price = 29.99f,
                    PublisherId = 2,
                    PublisherName = "Bantam Books",
                    ImageUrl = "url_to_image_2",
                    AuthorName = "George R.R. Martin",
                    CategoryId = 1, // Category: Fantasy
                    AuthorId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "The Hobbit",
                    Description = "A fantasy novel and prequel to The Lord of the Rings.",
                    Language = "English",
                    DatePublished = new DateTime(1937, 9, 21),
                    Price = 14.99f,
                    PublisherId = 3,
                    PublisherName = "HarperCollins",
                    ImageUrl = "url_to_image_3",
                    AuthorName = "J.R.R. Tolkien",
                    CategoryId = 1, // Category: Fantasy
                    AuthorId = 3
                },
                new Book
                {
                    Id = 4,
                    Title = "Murder on the Orient Express",
                    Description = "A mystery novel featuring detective Hercule Poirot.",
                    Language = "English",
                    DatePublished = new DateTime(1934, 1, 1),
                    Price = 12.99f,
                    PublisherId = 1,
                    PublisherName = "Bloomsbury Publishing",
                    ImageUrl = "url_to_image_4",
                    AuthorName = "Agatha Christie",
                    CategoryId = 3, // Category: Mystery
                    AuthorId = 4
                },
                new Book
                {
                    Id = 5,
                    Title = "The Shining",
                    Description = "A horror novel about a haunted hotel.",
                    Language = "English",
                    DatePublished = new DateTime(1977, 1, 28),
                    Price = 15.99f,
                    PublisherId = 2,
                    PublisherName = "Bantam Books",
                    ImageUrl = "url_to_image_5",
                    AuthorName = "Stephen King",
                    CategoryId = 4, // Category: Horror
                    AuthorId = 5
                }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Alice Johnson", Address = "123 Maple St", Email = "alice@example.com", Phone = "123-456-7890" },
                new Customer { Id = 2, Name = "Bob Smith", Address = "456 Oak St", Email = "bob@example.com", Phone = "987-654-3210" }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    CustomerId = 1
                },
                new Order
                {
                    Id = 2,
                    OrderDate = DateTime.Now,
                    CustomerId = 2
                }
            );

            // Seed BookOrders
            modelBuilder.Entity<BookOrder>().HasData(
                new BookOrder { Id = 1, Book_Id = 1, Order_Id = 1, Quantity = 2 },
                new BookOrder { Id = 2, Book_Id = 2, Order_Id = 1, Quantity = 1 },
                new BookOrder { Id = 3, Book_Id = 3, Order_Id = 2, Quantity = 1 },
                new BookOrder { Id = 4, Book_Id = 4, Order_Id = 2, Quantity = 3 }
            );
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }



    }
}
