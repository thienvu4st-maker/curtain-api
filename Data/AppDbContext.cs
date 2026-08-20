using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CategoryGroup> CategoryGroups => Set<CategoryGroup>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<ECatalog> ECatalogs => Set<ECatalog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure CategoryGroup -> Categories Relationship
        modelBuilder.Entity<CategoryGroup>()
            .HasMany(g => g.Categories)
            .WithOne(c => c.CategoryGroup)
            .HasForeignKey(c => c.CategoryGroupId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure Parent-Child self-referencing relationship
        modelBuilder.Entity<Category>()
            .HasOne(c => c.Parent)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
