using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class OrderRequest
    {
        public DateTime Date { get; set; }
        public virtual IEnumerable<OrderLineRequest> OrderLines { get; set; }
    }
}