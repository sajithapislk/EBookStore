using System;

namespace EBookStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Book Book { get; set; }
    }
}