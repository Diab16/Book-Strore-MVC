using AutoMapper;
using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_MVC.Controllers
{
    public class CartController1 : Controller
    {
        private readonly IBookRepository _bookRepository;

        public CartController1(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        private static List<CartItem> cart = new List<CartItem>();

        // Action to add an item to the cart
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            // Fetch the book from the repository
            var book = _bookRepository.GetById(productId);
            
            if (book != null)
            {
                var existingCartItem = cart.FirstOrDefault(item => item.ProductId == book.Id);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ProductId = book.Id,
                        Quantity = quantity,
                        ProductName = book.Title,
                        Price = book.Price,
                        imgfile = book.ImageUrl,

                    };



                    cart.Add(cartItem);
                }


            
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
