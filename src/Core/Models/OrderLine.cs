
namespace Core.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
    }
}