using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public virtual ProductType ProductType { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}