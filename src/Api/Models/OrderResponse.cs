using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class OrderResponse
    {
        public DateTime Date { get; set; }
        public virtual IEnumerable<OrderLineResponse> OrderLines { get; set; }
        public decimal RequiredBinWidth { get; set; }
    }
}