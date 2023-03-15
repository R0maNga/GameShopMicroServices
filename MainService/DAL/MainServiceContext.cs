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
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
        }
    }
}
