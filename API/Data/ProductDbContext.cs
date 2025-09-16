using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed the database with initial data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 1200.00m },
            new Product { Id = 2, Name = "Keyboard", Price = 80.00m },
            new Product { Id = 3, Name = "Mouse", Price = 25.50m }
        );
    }
}