using media_app_api.Data;
using media_app_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class ProductService(AppDbContext db) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await db.Products
            .AsNoTracking()
            .Select(p => new ProductDto(p.Id, p.Title, p.Price, p.Description, p.Category))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return await db.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductDto(p.Id, p.Title, p.Price, p.Description, p.Category))
            .FirstOrDefaultAsync();
    }
}
