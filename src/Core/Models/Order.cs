using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public decimal RequiredBinWidth 
        { 
            get
            {
                //group per product in case we find two different lines with the same product
                var groupedPerProduct = OrderLines.GroupBy(ol => ol.ProductType.Id);
                return groupedPerProduct.Sum
                (
                    gpp => gpp.First().ProductType.Width
                    *
                    (
                        Math.Ceiling(gpp.Sum(ol => (decimal)ol.Quantity) / gpp.First().ProductType.StackAmount)
                    )
                    
                );
            } 
        }

    }
}