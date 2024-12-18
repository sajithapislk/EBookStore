using EBookStore.Models;
using EBookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBookStore.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CartService cartService = new CartService();

        // GET: Search
        public ActionResult Search(string query)
        {
            var books = db.Books
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query) || b.Description.Contains(query))
                .ToList();
            return View(books);
        }
        // GET: Details
        public ActionResult Details(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult AddToCart(int id)
        {
            var book = db.Books.Find(id); // Fetch the book from the database

            if (book != null)
            {
                cartService.AddToCart(book); // Pass the book object to the service
                return RedirectToAction("Cart"); // Redirect to the Cart view after adding
            }

            // Optionally handle the case where the book is not found
            return HttpNotFound(); // Or redirect to an error page
        }

        public ActionResult UpdateCart(int id, int quantity)
        {
            cartService.UpdateCart(id, quantity);
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            var cartItems = cartService.GetCartItems();
            return View(cartItems);
        }

        public ActionResult Checkout()
        {
            // Process payment here
            // For demonstration, redirect to a confirmation view
            cartService.ClearCart(); // Clear cart after checkout
            return View("CheckoutConfirmation");
        }
    }
}