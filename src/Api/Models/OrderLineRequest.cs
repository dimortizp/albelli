
namespace Api.Models
{
    public class OrderLineRequest
    {
        public int Quantity { get; set; }

        public ProductTypeRequest ProductType { get; set; }
    }
}