
using System.Linq;
using Data.Entities;

namespace Data.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(OrderManagerContext context)
        {
            context.Database.EnsureCreated();

            if (context.ProductTypes.Any())
            {
                return;
            }

            var productTypes = new ProductType[]
            {
                new ProductType
                {
                    Name = "photoBook",
                    StackAmount = 1,
                    Width = 19
                },
                new ProductType
                {
                    Name = "calendar",
                    StackAmount = 1,
                    Width = 10
                },
                new ProductType
                {
                    Name = "canvas",
                    StackAmount = 1,
                    Width = 16
                },
                new ProductType
                {
                    Name = "cards",
                    StackAmount = 1,
                    Width = 4.7m
                },
                new ProductType
                {
                    Name = "mug",
                    StackAmount = 4,
                    Width = 94
                }
            };

            context.ProductTypes.AddRange(productTypes);

            context.SaveChanges();
        }
    }
}
