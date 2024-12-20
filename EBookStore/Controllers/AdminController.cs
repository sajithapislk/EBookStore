using EBookStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBookStore.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and Password are required.");
                return View();
            }

            var admin = db.Admins.FirstOrDefault(c => c.Email == email);
            if (admin != null)
            {
                var passwordHasher = new PasswordHasher();
                var verificationResult = passwordHasher.VerifyHashedPassword(admin.Password, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    Session["AdminId"] = admin.AdminId;
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }
        public ActionResult Dashboard()
        {
            // Total Books
            ViewBag.TotalBooks = 1000;

            // Total Customers
            ViewBag.TotalCustomers = 1000;

            // Total Publishers
            ViewBag.TotalPublishers = 1000;

            // Total Orders
            ViewBag.TotalOrders = 1000;

            // Total Payment
            ViewBag.TotalPayments = 1000;

            // Pending Orders
            ViewBag.TotalPendingOrders = 1000;

            // Pending Payments
            ViewBag.TotalPendingPayments = 1000;

            // Total Authors
            ViewBag.TotalAuthors = 100;

            ViewBag.TotalEarnings = 1000;

            return View();
        }
    }
}