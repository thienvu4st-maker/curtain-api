using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await db.Categories
            .AsNoTracking()
            .Include(c => c.Parent)
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .ToListAsync();

        return categories.Select(c => MapToDto(c));
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await db.Categories
            .AsNoTracking()
            .Include(c => c.Parent)
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) return null;

        return MapToDto(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description ?? string.Empty,
            IconName = request.IconName ?? "curtain",
            ParentId = request.ParentId,
            CreatedAt = DateTime.UtcNow
        };

        db.Categories.Add(category);
        await db.SaveChangesAsync();

        string? parentName = null;
        if (request.ParentId.HasValue)
        {
            var parent = await db.Categories.FindAsync(request.ParentId.Value);
            parentName = parent?.Name;
        }

        return new CategoryDto(category.Id, category.Name, category.Description, category.IconName, category.ParentId, parentName, 0, []);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto request)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return null;

        category.Name = request.Name;
        category.Description = request.Description ?? string.Empty;
        category.ParentId = request.ParentId;

        if (!string.IsNullOrEmpty(request.IconName))
        {
            category.IconName = request.IconName;
        }

        await db.SaveChangesAsync();

        string? parentName = null;
        if (request.ParentId.HasValue)
        {
            var parent = await db.Categories.FindAsync(request.ParentId.Value);
            parentName = parent?.Name;
        }

        var count = await db.Products.CountAsync(p => p.CategoryId == id);
        return new CategoryDto(category.Id, category.Name, category.Description, category.IconName, category.ParentId, parentName, count, []);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return false;

        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }

    private static CategoryDto MapToDto(Category c)
    {
        return new CategoryDto(
            c.Id,
            c.Name,
            c.Description,
            c.IconName,
            c.ParentId,
            c.Parent?.Name,
            c.Products.Count,
            c.SubCategories.Select(s => MapToDto(s))
        );
    }
}
