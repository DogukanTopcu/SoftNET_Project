using Microsoft.EntityFrameworkCore;
using SoftNET_Project.Models;

namespace SoftNET_Project.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Role { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<UserCarts>().HasKey(uc => new { uc.UserId, uc.ProductId });
            modelBuilder.Entity<UserWishlist>().HasKey(uc => new { uc.UserId, uc.ProductId });
            modelBuilder.Entity<UserPurchased>().HasKey(uc => new { uc.UserId, uc.ProductId });
        }

    }
}
