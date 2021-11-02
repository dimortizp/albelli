using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual IEnumerable<OrderLine> OrderLines { get; set; }
    }
}