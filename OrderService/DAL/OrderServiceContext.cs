using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class OrderServiceContext:DbContext
    {
        public OrderServiceContext(DbContextOptions<OrderServiceContext> options):base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}