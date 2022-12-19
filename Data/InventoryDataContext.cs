﻿using ApiInventoryControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInventoryControl.Data
{
    public class InventoryDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=ApiLogin;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
    }
}
