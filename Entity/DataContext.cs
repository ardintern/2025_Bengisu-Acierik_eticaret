using Microsoft.EntityFrameworkCore;

namespace EcommerceWebSite.Entity
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderLine> Orderlines { get; set; } = default!;

    }
}
