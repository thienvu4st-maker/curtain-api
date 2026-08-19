using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Seed Real Curtain Categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Rèm Vải",
                Description = "Rèm vải gấm, lụa, voan chống nắng 100% dành cho phòng khách và phòng ngủ.",
                IconName = "curtain",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 2,
                Name = "Rèm Cuốn",
                Description = "Rèm cuốn văn phòng, chống nắng cách nhiệt hiện đại gọn gàng.",
                IconName = "blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 3,
                Name = "Rèm Gỗ",
                Description = "Rèm sáo gỗ tự nhiên cao cấp mang lại vẻ đẹp sang trọng ấm cúng.",
                IconName = "wooden_blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 4,
                Name = "Rèm Cầu Vồng",
                Description = "Rèm cầu vồng Hàn Quốc thiết kế 2 lớp điều chỉnh ánh sáng linh hoạt.",
                IconName = "rainbow_blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // 2. Seed Real Curtain Products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Rèm Vải 2 Lớp Chống Nắng Cao Cấp",
                Price = 850000m,
                Description = "Rèm vải 2 lớp kết hợp lớp voan thêu tay nhẹ nhàng và lớp gấm chống nắng 100%.",
                ImageUrl = "https://images.unsplash.com/photo-1513694203232-719a280e022f?w=600",
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                Title = "Rèm Cuốn Văn Phòng Trơn Tráng Bạc",
                Price = 320000m,
                Description = "Rèm cuốn trơn chất liệu Polyester phủ lớp tráng bạc chống tia UV hiệu quả.",
                ImageUrl = "https://images.unsplash.com/photo-1540518614846-7eded433c457?w=600",
                CategoryId = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                Title = "Rèm Sáo Gỗ Sồi Nga Tự Nhiên",
                Price = 680000m,
                Description = "Rèm gỗ tự nhiên bản lá 5cm đã qua xử lý hấp sấy chống mối mọt cong vênh.",
                ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=600",
                CategoryId = 3,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 4,
                Title = "Rèm Cầu Vồng Hàn Quốc Modero",
                Price = 520000m,
                Description = "Rèm cầu vồng nhập khẩu Hàn Quốc hệ thanh nhôm cao cấp xoay 180 độ.",
                ImageUrl = "https://images.unsplash.com/photo-1522771739844-6a9f6d5f14af?w=600",
                CategoryId = 4,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
