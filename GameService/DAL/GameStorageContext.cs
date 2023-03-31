using System.Security.Cryptography.X509Certificates;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GameStorageContext:DbContext
    {
        public GameStorageContext(DbContextOptions<GameStorageContext> options) : base(options)
        {

        }
        public DbSet<GameStorage> GameStorages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameStorage>().HasKey(i => i.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}