using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class MainServiceContext : DbContext
    {
        public MainServiceContext(DbContextOptions<MainServiceContext> options) : base(options)
        {

        }

        public DbSet<User> UserEntities { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<BasketToGame> BasketToGames { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().HasOne(e => e.Basket).WithOne(e => e.User)
                .HasForeignKey<Basket>(e=>e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Basket>().HasKey(e => e.Id);

            modelBuilder.Entity<Game>().HasKey(e => e.Id);

            modelBuilder.Entity<BasketToGame>().HasOne(e => e.Game).WithMany(e => e.BasketToGame)
                .HasForeignKey(e => e.GameId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BasketToGame>().HasOne(e => e.Basket).WithMany(e => e.BasketToGame)
                .HasForeignKey(e => e.BasketId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
