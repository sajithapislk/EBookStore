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

        // GET: Search
        public ActionResult Search(string query)
        {
            var books = db.Books
                .Where(b => b.Title.Contains(query) || b.Description.Contains(query))
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

    }
}