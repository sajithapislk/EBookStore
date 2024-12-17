using EBookStore.Models;
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

        // GET: Search
        public ActionResult Search(string query)
        {
            var books = db.Books
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query) || b.Description.Contains(query))
                .ToList();
            return View(books);
        }
    }
}