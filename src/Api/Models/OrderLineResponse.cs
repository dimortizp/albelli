
namespace Api.Models
{
    public class OrderLineResponse
    {
        public int Quantity { get; set; }

        public ProductTypeResponse ProductType { get; set; }
    }
}