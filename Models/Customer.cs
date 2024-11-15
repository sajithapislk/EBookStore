using System;
using System.Collections.Generic;

namespace EBookStore.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}