
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Width { get; set; }
        public int StackAmount { get; set; }
    }
}