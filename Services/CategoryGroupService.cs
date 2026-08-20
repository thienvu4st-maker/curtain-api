using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class CategoryGroupService(AppDbContext dbContext) : ICategoryGroupService
{
    public async Task<IEnumerable<CategoryGroupDto>> GetCategoryGroupsAsync()
    {
        var groups = await dbContext.CategoryGroups
            .AsNoTracking()
            .Include(g => g.Categories)
            .ThenInclude(c => c.Products)
            .OrderBy(g => g.Id)
            .ToListAsync();

        return groups.Select(g => new CategoryGroupDto(
            g.Id,
            g.Name,
            g.Description,
            g.IconName,
            g.Categories.Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Description,
                c.IconName,
                c.CategoryGroupId,
                g.Name,
                c.ParentId,
                null,
                c.Products.Count,
                []
            ))
        ));
    }

    public async Task<CategoryGroupDto?> GetCategoryGroupByIdAsync(int id)
    {
        var g = await dbContext.CategoryGroups
            .AsNoTracking()
            .Include(g => g.Categories)
            .ThenInclude(c => c.Products)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (g is null) return null;

        return new CategoryGroupDto(
            g.Id,
            g.Name,
            g.Description,
            g.IconName,
            g.Categories.Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Description,
                c.IconName,
                c.CategoryGroupId,
                g.Name,
                c.ParentId,
                null,
                c.Products.Count,
                []
            ))
        );
    }

    public async Task<CategoryGroupDto> CreateCategoryGroupAsync(CreateCategoryGroupDto request)
    {
        var group = new CategoryGroup
        {
            Name = request.Name,
            Description = request.Description,
            IconName = request.IconName
        };

        dbContext.CategoryGroups.Add(group);
        await dbContext.SaveChangesAsync();

        return new CategoryGroupDto(group.Id, group.Name, group.Description, group.IconName, []);
    }

    public async Task<CategoryGroupDto?> UpdateCategoryGroupAsync(int id, UpdateCategoryGroupDto request)
    {
        var group = await dbContext.CategoryGroups.FindAsync(id);
        if (group is null) return null;

        group.Name = request.Name;
        group.Description = request.Description;
        group.IconName = request.IconName;

        await dbContext.SaveChangesAsync();

        return new CategoryGroupDto(group.Id, group.Name, group.Description, group.IconName, []);
    }

    public async Task<bool> DeleteCategoryGroupAsync(int id)
    {
        var group = await dbContext.CategoryGroups.FindAsync(id);
        if (group is null) return false;

        dbContext.CategoryGroups.Remove(group);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
