using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure
{
    public class OrderManagerContext : DbContext
    {
        public OrderManagerContext(DbContextOptions<OrderManagerContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderLine>().ToTable("OrderLine");
            modelBuilder.Entity<ProductType>().ToTable("ProductType");
        }
    }
}
