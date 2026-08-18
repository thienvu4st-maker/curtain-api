using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class ProductService(AppDbContext db) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? categoryId = null)
    {
        var query = db.Products
            .Include(p => p.Category)
            .AsNoTracking();

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        return await query
            .Select(p => new ProductDto(
                p.Id,
                p.Title,
                p.Price,
                p.Description,
                p.ImageUrl,
                p.CategoryId,
                p.Category != null ? p.Category.Name : "Chưa phân loại"
            ))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return await db.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductDto(
                p.Id,
                p.Title,
                p.Price,
                p.Description,
                p.ImageUrl,
                p.CategoryId,
                p.Category != null ? p.Category.Name : "Chưa phân loại"
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto request)
    {
        var product = new Product
        {
            Title = request.Title,
            Price = request.Price,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        string categoryName = "Chưa phân loại";
        if (request.CategoryId.HasValue)
        {
            var cat = await db.Categories.FindAsync(request.CategoryId.Value);
            if (cat != null) categoryName = cat.Name;
        }

        return new ProductDto(
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.CategoryId,
            categoryName
        );
    }
}
