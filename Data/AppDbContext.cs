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

        // 1. Seed Category Groups (Chuyên Đề)
        modelBuilder.Entity<CategoryGroup>().HasData(
            new CategoryGroup
            {
                Id = 1,
                Name = "Rèm Cửa & Màn Cửa",
                Description = "Đầy đủ các loại rèm vải, rèm cuốn, rèm cầu vồng, rèm gỗ, rèm tổ ong, roman & rèm tự động.",
                IconName = "curtains"
            },
            new CategoryGroup
            {
                Id = 2,
                Name = "Ốp Tường, Trần & Trang Trí",
                Description = "Đầy đủ tấm ốp nhựa PVC vân đá, lam sóng, ốp Nano, giấy dán tường Hàn Quốc & gỗ nhựa ngoài trời.",
                IconName = "wall"
            },
            new CategoryGroup
            {
                Id = 3,
                Name = "Dịch Vụ & Bảo Trì",
                Description = "Dịch vụ tháo lắp giặt hấp màn rèm tận nhà & sửa chữa thay thế phụ kiện rèm cửa.",
                IconName = "cleaning_services"
            }
        );

        // 2. Seed Categories linked to CategoryGroups
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 10,
                CategoryGroupId = 1,
                Name = "Rèm Vải 2 Lớp (Gấm & Voan)",
                Description = "Phối hợp giữa voan thêu mềm mại và vải gấm cản sáng 100% cho phòng khách & phòng ngủ.",
                IconName = "curtain",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 11,
                CategoryGroupId = 1,
                Name = "Rèm Voan & Voan Thêu Tay",
                Description = "Voan trắng trơn, voan xước & voan thêu nghệ thuật nhẹ nhàng lãng mạn.",
                IconName = "voan",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 12,
                CategoryGroupId = 1,
                Name = "Rèm Vải Tân Cổ Điển / Nữ Hoàng",
                Description = "Rèm vải may bèo nhún yếm sò quý phái cho biệt thự tân cổ điển.",
                IconName = "royal",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 13,
                CategoryGroupId = 1,
                Name = "Rèm Cuốn Văn Phòng (Chống Nắng 100%)",
                Description = "Rèm cuốn trơn tráng bạc cản sáng cản nhiệt 100% cho văn phòng.",
                IconName = "blinds",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 14,
                CategoryGroupId = 1,
                Name = "Rèm Cuốn Lưới & In Tranh 3D",
                Description = "Rèm cuốn lưới nhìn xuyên không gian & rèm cuốn in tranh 3D nghệ thuật.",
                IconName = "blinds_mesh",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 15,
                CategoryGroupId = 1,
                Name = "Rèm Cầu Vồng Hàn Quốc (Modero/Combi)",
                Description = "Rèm cầu vồng nhập khẩu Hàn Quốc 2 lớp xoay lật điều chỉnh ánh sáng 180 độ.",
                IconName = "rainbow",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 16,
                CategoryGroupId = 1,
                Name = "Rèm Sáo Gỗ & Rèm Sáo Nhôm",
                Description = "Rèm sáo gỗ sồi Nga tự nhiên bản 5cm & rèm sáo nhôm chống nước.",
                IconName = "wooden",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 17,
                CategoryGroupId = 1,
                Name = "Rèm Tổ Ong & Vách Ngăn Tổ Ong",
                Description = "Rèm tổ ong cản nhiệt 100% & hệ vách ngăn tổ ong di động thông minh.",
                IconName = "honeycomb",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 18,
                CategoryGroupId = 1,
                Name = "Rèm Roman Xếp Lớp",
                Description = "Rèm Roman may xếp lớp hiện đại tiết kiệm diện tích cho cửa sổ nhỏ.",
                IconName = "roman",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 19,
                CategoryGroupId = 1,
                Name = "Rèm Lá Dọc Văn Phòng",
                Description = "Rèm lá dọc xoay lật 180 độ giá rẻ cản sáng hiệu quả cho văn phòng.",
                IconName = "vertical",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 20,
                CategoryGroupId = 1,
                Name = "Rèm Tự Động Thông Minh",
                Description = "Động cơ rèm cửa tự động tích hợp điều khiển từ xa, remote & App Smarthome.",
                IconName = "smart",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 21,
                CategoryGroupId = 2,
                Name = "Tấm Ốp Nhựa PVC Vân Đá Tráng Gương",
                Description = "Tấm ốp nhựa PVC giả đá cẩm thạch tráng gương sáng bóng, chống ẩm mốc 100%.",
                IconName = "pvc_wall",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 22,
                CategoryGroupId = 2,
                Name = "Tấm Ốp Lam Sóng Trang Trí Vách TV",
                Description = "Tấm ốp lam sóng nhựa PVC/PS cốt nguyên sinh E0 tạo điểm nhấn vách TV phòng khách.",
                IconName = "lam_song",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 23,
                CategoryGroupId = 2,
                Name = "Tấm Ốp Nhựa Nano Vân Gỗ & Hoa Văn",
                Description = "Tấm ốp Nano phẳng hèm khóa giấu nẹp, vân gỗ tự nhiên & hoa văn trang trí trần tường.",
                IconName = "nano_panel",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 24,
                CategoryGroupId = 2,
                Name = "Giấy Dán Tường Hàn Quốc & Tranh 3D",
                Description = "Giấy dán tường Hàn Quốc chính hãng & tranh dán tường 3D khổ lớn in theo kích thước.",
                IconName = "wallpaper",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 25,
                CategoryGroupId = 2,
                Name = "Sàn Nhựa Hèm Khóa & Gỗ Nhựa Ngoài Trời",
                Description = "Sàn nhựa SPC hèm khóa 4mm-6mm & lam gỗ nhựa ngoài trời chịu mưa nắng.",
                IconName = "outdoor_wood",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 26,
                CategoryGroupId = 3,
                Name = "Dịch Vụ Giặt Màn Rèm Tận Nhà",
                Description = "Tháo rèm tận nhà, mang về giặt hấp khử khuẩn và lắp lại hoàn chỉnh trong ngày.",
                IconName = "curtain_wash",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 27,
                CategoryGroupId = 3,
                Name = "Sửa Chữa & Thay Phụ Kiện Rèm Cửa",
                Description = "Sửa rèm kẹt, thay thanh treo, dây kéo & phụ kiện màn rèm cũ.",
                IconName = "repair",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // 3. Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Rèm Vải 2 Lớp Chống Nắng 100% Nhật Bản",
                Price = 0m,
                Description = "Sự kết hợp giữa vải gấm dệt kim cao cấp cản sáng tuyệt đối và lớp voan trắng mềm mại.",
                ImageUrl = "https://images.unsplash.com/photo-1513694203232-719a280e022f?w=600",
                CategoryId = 10,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                Title = "Rèm Voan Thêu Tay Hoa Văn Tinh Tế",
                Price = 0m,
                Description = "Lớp voan thêu họa tiết nghệ thuật điểm nhẹ sang trọng cho cửa sổ phòng khách.",
                ImageUrl = "https://images.unsplash.com/photo-1540518614846-7eded433c457?w=600",
                CategoryId = 11,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                Title = "Rèm Cầu Vồng Hàn Quốc Modero Cao Cấp",
                Price = 0m,
                Description = "Mẫu rèm cầu vồng 2 lớp điều chỉnh ánh sáng xoay lật linh hoạt nhập khẩu Hàn Quốc.",
                ImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=600",
                CategoryId = 15,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 4,
                Title = "Tấm Ốp Tường Nhựa PVC Vân Đá Tráng Gương",
                Price = 0m,
                Description = "Bề mặt phủ UV tráng gương bóng đẹp như đá cẩm thạch tự nhiên, chống ẩm mốc.",
                ImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=600",
                CategoryId = 21,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 5,
                Title = "Tấm Ốp Lam Sóng Nhựa Giả Gỗ Trang Trí Vách TV",
                Price = 0m,
                Description = "Thiết kế 4 sóng cao sang trọng, cốt nhựa nguyên sinh E0 an toàn không mùi hôi.",
                ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=600",
                CategoryId = 22,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 6,
                Title = "Dịch Vụ Giặt Màn Rèm Hấp Khử Khuẩn Tận Nhà",
                Price = 0m,
                Description = "Đội ngũ thợ đến tháo rèm tận nhà, mang về giặt hấp hơi nước nóng khử khuẩn 99.9%.",
                ImageUrl = "https://images.unsplash.com/photo-1582735689369-4fe89db7114c?w=600",
                CategoryId = 26,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // 4. Seed ECatalogs (Mẫu Chọn Màu Điện Tử 2025-2026)
        modelBuilder.Entity<ECatalog>().HasData(
            new ECatalog
            {
                Id = 1,
                Title = "SHIREN Interior Curtain Collection 2025 - 2026 (Vol 12)",
                Description = "Bộ sưu tập Catalogue mẫu vải rèm gấm cản sáng, voan thêu nghệ thuật nhập khẩu Nhật Bản & Châu Âu.",
                CoverImageUrl = "https://images.unsplash.com/photo-1513694203232-719a280e022f?w=600",
                PdfUrl = "https://heyzine.com/flip-book/1a2b3c4d5e.html",
                CategoryGroupId = 1,
                CategoryId = 10,
                PageCount = 41,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ECatalog
            {
                Id = 2,
                Title = "MODERO Korea Luxury Blinds & Shades 2025",
                Description = "Catalogue điện tử chọn mẫu rèm cầu vồng, rèm cuốn tráng bạc & rèm tổ ong cao cấp Hàn Quốc.",
                CoverImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=600",
                PdfUrl = "https://heyzine.com/flip-book/modero-korea-2025.html",
                CategoryGroupId = 1,
                CategoryId = 15,
                PageCount = 68,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ECatalog
            {
                Id = 3,
                Title = "KOSMOS Tấm Ốp PVC Vân Đá & Lam Sóng 2025",
                Description = "Catalogue tổng hợp các mẫu tấm ốp PVC tráng gương vân đá cẩm thạch & lam sóng ốp vách TV.",
                CoverImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=600",
                PdfUrl = "https://heyzine.com/flip-book/kosmos-pvc-2025.html",
                CategoryGroupId = 2,
                CategoryId = 21,
                PageCount = 52,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
