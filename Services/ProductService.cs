using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class ProductService(AppDbContext dbContext) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? categoryId = null)
    {
        var query = dbContext.Products.Include(p => p.Category).AsQueryable();

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        var products = await query.ToListAsync();

        return products.Select(p => new ProductDto(
            p.Id,
            p.Title,
            p.Price,
            p.Description,
            p.ImageUrl,
            p.CategoryId,
            p.Category?.Name ?? "Chưa phân loại"
        ));
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var p = await dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (p is null) return null;

        return new ProductDto(
            p.Id,
            p.Title,
            p.Price,
            p.Description,
            p.ImageUrl,
            p.CategoryId,
            p.Category?.Name ?? "Chưa phân loại"
        );
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto request)
    {
        var product = new Product
        {
            Title = request.Title,
            Price = request.Price,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        var category = request.CategoryId.HasValue
            ? await dbContext.Categories.FindAsync(request.CategoryId.Value)
            : null;

        return new ProductDto(
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.CategoryId,
            category?.Name ?? "Chưa phân loại"
        );
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto request)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null) return null;

        product.Title = request.Title;
        product.Price = request.Price;
        product.Description = request.Description;
        product.ImageUrl = request.ImageUrl;
        product.CategoryId = request.CategoryId;

        await dbContext.SaveChangesAsync();

        var category = request.CategoryId.HasValue
            ? await dbContext.Categories.FindAsync(request.CategoryId.Value)
            : null;

        return new ProductDto(
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.CategoryId,
            category?.Name ?? "Chưa phân loại"
        );
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null) return false;

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
