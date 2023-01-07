using ApiInventoryControl.Data.Mapping;
using ApiInventoryControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInventoryControl.Data
{
    public class InventoryDataContext : DbContext
    {
        public InventoryDataContext(DbContextOptions<InventoryDataContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
