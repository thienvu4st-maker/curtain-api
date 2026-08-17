using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "Flutter Developer Handbook", Price = 29.99m, Description = "A comprehensive guide to building cross-platform apps with Dart and Flutter.", Category = "Books", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Title = "Wireless Noise-Canceling Headphones", Price = 199.50m, Description = "High-fidelity audio with active noise cancellation and 30-hour battery life.", Category = "Electronics", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Title = "Ergonomic Mechanical Keyboard", Price = 129.00m, Description = "Tactile switches with customizable RGB backlighting for developers.", Category = "Peripherals", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Title = "Smart Fitness Watch", Price = 149.99m, Description = "Track heart rate, sleep quality, and workouts with integrated GPS.", Category = "Wearables", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
