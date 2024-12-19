using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity; // For using ASP.NET Identity's PasswordHasher

namespace EBookStore.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Customer/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Customer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher();
                customer.Password = passwordHasher.HashPassword(customer.Password); // Use ASP.NET Identity's PasswordHasher
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(customer);
        }

        // POST: Customer/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and Password are required.");
                return View();
            }

            var customer = db.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                var passwordHasher = new PasswordHasher();
                var verificationResult = passwordHasher.VerifyHashedPassword(customer.Password, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    Session["CustomerId"] = customer.CustomerId;
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }
    }
}