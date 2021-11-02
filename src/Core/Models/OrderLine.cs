
namespace Core.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}