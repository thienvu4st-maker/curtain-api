using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        return await db.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Description,
                c.IconName,
                c.Products.Count
            ))
            .ToListAsync();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await db.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) return null;

        return new CategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.IconName,
            category.Products.Count
        );
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description ?? string.Empty,
            IconName = request.IconName ?? "curtain",
            CreatedAt = DateTime.UtcNow
        };

        db.Categories.Add(category);
        await db.SaveChangesAsync();

        return new CategoryDto(category.Id, category.Name, category.Description, category.IconName, 0);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto request)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return null;

        category.Name = request.Name;
        category.Description = request.Description ?? string.Empty;
        if (!string.IsNullOrEmpty(request.IconName))
        {
            category.IconName = request.IconName;
        }

        await db.SaveChangesAsync();

        var count = await db.Products.CountAsync(p => p.CategoryId == id);
        return new CategoryDto(category.Id, category.Name, category.Description, category.IconName, count);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return false;

        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }
}
