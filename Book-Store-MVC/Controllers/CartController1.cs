using AutoMapper;
using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_MVC.Controllers
{
    public class CartController1 : Controller
    {
        private readonly BookStoreContext bookStore;

        public CartController1(BookStoreContext bookStore)
        {
            this.bookStore = bookStore;
        }

        private static List<CartItem> cart = new List<CartItem>();

        // Action to add an item to the cart
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            // Fetch the book from the database using the DbSet<Book>
            var book = bookStore.Books.Find(productId); // Assuming Books is your DbSet<Book>

            if (book != null)
            {
                var cartItem = new CartItem
                {
                    ProductId = book.Id,
                    Quantity = quantity,
                    ProductName = book.Title, // Ensure this matches your Book model
                    Price = book.Price // Use the price from the book
                };
                cart.Add(cartItem);
            }

            return RedirectToAction("ViewCart");
        }

        // Action to view the cart
        public ActionResult ViewCart()
        {
            return View(cart);
        }

        // Action to remove an item from the cart
        public ActionResult RemoveFromCart(int productId)
        {
            var itemToRemove = cart.Find(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            return RedirectToAction("ViewCart");
        }
    }


}
