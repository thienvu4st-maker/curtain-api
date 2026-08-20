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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Seed Category Groups (Chuyên Đề)
        modelBuilder.Entity<CategoryGroup>().HasData(
            new CategoryGroup
            {
                Id = 1,
                Name = "Màn Rèm Cửa",
                Description = "Chuyên đề rèm vải, rèm cuốn, rèm gỗ, rèm cầu vồng các loại.",
                IconName = "curtains"
            },
            new CategoryGroup
            {
                Id = 2,
                Name = "Ốp Tường & Trang Trí",
                Description = "Chuyên đề tấm ốp nhựa PVC vân đá, lam sóng trang trí, giấy dán tường Hàn Quốc.",
                IconName = "wall"
            },
            new CategoryGroup
            {
                Id = 3,
                Name = "Dịch Vụ & Bảo Trì",
                Description = "Chuyên đề dịch vụ tháo lắp, giặt hấp màn rèm & bảo dưỡng trang thiết bị.",
                IconName = "cleaning_services"
            }
        );

        // 2. Seed Detailed Categories (Danh Mục Chi Tiết) linked to Category Groups
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                CategoryGroupId = 1,
                Name = "Rèm Vải 2 Lớp",
                Description = "Rèm vải gấm, lụa, voan chống nắng 100% dành cho phòng khách và phòng ngủ.",
                IconName = "curtain",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 2,
                CategoryGroupId = 1,
                Name = "Rèm Cuốn Văn Phòng",
                Description = "Rèm cuốn văn phòng, chống nắng cách nhiệt hiện đại gọn gàng.",
                IconName = "blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 3,
                CategoryGroupId = 1,
                Name = "Rèm Gỗ & Rèm Sáo",
                Description = "Rèm sáo gỗ tự nhiên cao cấp mang lại vẻ đẹp sang trọng ấm cúng.",
                IconName = "wooden_blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 4,
                CategoryGroupId = 1,
                Name = "Rèm Cầu Vồng Hàn Quốc",
                Description = "Rèm cầu vồng Hàn Quốc thiết kế 2 lớp điều chỉnh ánh sáng linh hoạt.",
                IconName = "rainbow_blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 5,
                CategoryGroupId = 2,
                Name = "Tấm Ốp PVC Vân Đá",
                Description = "Tấm ốp nhựa PVC giả đá cẩm thạch tráng gương chống ẩm mốc.",
                IconName = "pvc_wall",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 6,
                CategoryGroupId = 2,
                Name = "Tấm Ốp Lam Sóng",
                Description = "Tấm ốp lam sóng nhựa PVC giả gỗ trang trí vách TV & tường.",
                IconName = "lam_song",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 7,
                CategoryGroupId = 2,
                Name = "Giấy Dán Tường Hàn Quốc",
                Description = "Giấy dán tường Hàn Quốc & tranh 3D dán tường cao cấp.",
                IconName = "wallpaper",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 8,
                CategoryGroupId = 3,
                Name = "Dịch Vụ Giặt Màn Rèm Tận Nhà",
                Description = "Dịch vụ tháo lắp, giặt hấp khử khuẩn màn rèm cửa tận nhà trong ngày.",
                IconName = "curtain_wash",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // 3. Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Rèm Vải 2 Lớp Chống Nắng Cao Cấp",
                Price = 0m,
                Description = "Rèm vải 2 lớp kết hợp lớp voan thêu tay nhẹ nhàng và lớp gấm chống nắng 100%.",
                ImageUrl = "https://images.unsplash.com/photo-1513694203232-719a280e022f?w=600",
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                Title = "Rèm Cuốn Văn Phòng Trơn Tráng Bạc",
                Price = 0m,
                Description = "Rèm cuốn trơn chất liệu Polyester phủ lớp tráng bạc chống tia UV hiệu quả.",
                ImageUrl = "https://images.unsplash.com/photo-1540518614846-7eded433c457?w=600",
                CategoryId = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                Title = "Rèm Sáo Gỗ Sồi Nga Tự Nhiên",
                Price = 0m,
                Description = "Rèm gỗ tự nhiên bản lá 5cm đã qua xử lý hấp sấy chống mối mọt cong vênh.",
                ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=600",
                CategoryId = 3,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 4,
                Title = "Rèm Cầu Vồng Hàn Quốc Modero",
                Price = 0m,
                Description = "Rèm cầu vồng nhập khẩu Hàn Quốc hệ thanh nhôm cao cấp xoay 180 độ.",
                ImageUrl = "https://images.unsplash.com/photo-1522771739844-6a9f6d5f14af?w=600",
                CategoryId = 4,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 5,
                Title = "Tấm Ốp Tường Nhựa PVC Vân Đá Tráng Gương",
                Price = 0m,
                Description = "Tấm ốp nhựa PVC tráng gương sáng bóng như đá tự nhiên chống ẩm mốc.",
                ImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=600",
                CategoryId = 5,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 6,
                Title = "Tấm Ốp Lam Sóng Nhựa Giả Gỗ Vách TV",
                Price = 0m,
                Description = "Tấm ốp lam sóng cốt nhựa PVC nguyên sinh E0 tạo vách nhấn trang trí TV.",
                ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=600",
                CategoryId = 6,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 7,
                Title = "Giấy Dán Tường Hàn Quốc Họa Tiết Tân Cổ Điển",
                Price = 0m,
                Description = "Mẫu hoa văn chìm tinh tế bề mặt phủ Vinyl chùi rửa dễ dàng.",
                ImageUrl = "https://images.unsplash.com/photo-1615873968403-89e068629265?w=600",
                CategoryId = 7,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 8,
                Title = "Dịch Vụ Giặt Màn Rèm Hấp Khử Khuẩn Tận Nhà",
                Price = 0m,
                Description = "Tháo rèm tận nhà, mang về giặt hấp khử khuẩn và tháo lắp lại hoàn chỉnh trong ngày.",
                ImageUrl = "https://images.unsplash.com/photo-1582735689369-4fe89db7114c?w=600",
                CategoryId = 8,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
