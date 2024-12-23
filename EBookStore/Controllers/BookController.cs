using EBookStore.Models;
using EBookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult AdminIndex()
        {
            var books = db.Books.Include("Author").ToList();
            //return Json(books, JsonRequestBehavior.AllowGet);
            return View(books);
        }
        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Include("Author").FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }
        public ActionResult AdminCreate()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate([Bind(Include = "BookId,AuthorId,Title,Image,ISBN,Price,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View(book);
        }

        public ActionResult AdminEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEdit([Bind(Include = "BookId,AuthorId,Title,Image,ISBN,Price,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult AdminDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult AdminDeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult BookReport()
        {
            var books = db.Books.Include("Author").ToList();
            return new Rotativa.ViewAsPdf("BookReport", books)
            {
                FileName = "BookReport.pdf"
            };
        }

    }
}