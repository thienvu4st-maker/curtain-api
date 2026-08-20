using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class ECatalogService(AppDbContext db) : IECatalogService
{
    public async Task<IEnumerable<ECatalogDto>> GetECatalogsAsync(int? categoryGroupId = null, int? categoryId = null)
    {
        var query = db.ECatalogs
            .Include(e => e.CategoryGroup)
            .Include(e => e.Category)
            .AsNoTracking();

        if (categoryGroupId.HasValue && categoryGroupId.Value > 0)
        {
            query = query.Where(e => e.CategoryGroupId == categoryGroupId.Value);
        }

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(e => e.CategoryId == categoryId.Value);
        }

        var items = await query.OrderByDescending(e => e.Id).ToListAsync();

        return items.Select(MapToDto);
    }

    public async Task<ECatalogDto?> GetECatalogByIdAsync(int id)
    {
        var item = await db.ECatalogs
            .Include(e => e.CategoryGroup)
            .Include(e => e.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        return item is null ? null : MapToDto(item);
    }

    public async Task<ECatalogDto> CreateECatalogAsync(CreateECatalogDto dto)
    {
        var item = new ECatalog
        {
            Title = dto.Title,
            Description = dto.Description,
            CoverImageUrl = dto.CoverImageUrl,
            PdfUrl = dto.PdfUrl,
            CategoryGroupId = dto.CategoryGroupId,
            CategoryId = dto.CategoryId,
            PageCount = dto.PageCount > 0 ? dto.PageCount : 1,
            CreatedAt = DateTime.UtcNow
        };

        db.ECatalogs.Add(item);
        await db.SaveChangesAsync();

        return (await GetECatalogByIdAsync(item.Id))!;
    }

    public async Task<ECatalogDto?> UpdateECatalogAsync(int id, UpdateECatalogDto dto)
    {
        var item = await db.ECatalogs.FindAsync(id);
        if (item is null) return null;

        item.Title = dto.Title;
        item.Description = dto.Description;
        item.CoverImageUrl = dto.CoverImageUrl;
        item.PdfUrl = dto.PdfUrl;
        item.CategoryGroupId = dto.CategoryGroupId;
        item.CategoryId = dto.CategoryId;
        item.PageCount = dto.PageCount > 0 ? dto.PageCount : 1;

        await db.SaveChangesAsync();
        return (await GetECatalogByIdAsync(id))!;
    }

    public async Task<bool> DeleteECatalogAsync(int id)
    {
        var item = await db.ECatalogs.FindAsync(id);
        if (item is null) return false;

        db.ECatalogs.Remove(item);
        await db.SaveChangesAsync();
        return true;
    }

    private static ECatalogDto MapToDto(ECatalog e) => new(
        e.Id,
        e.Title,
        e.Description,
        e.CoverImageUrl,
        e.PdfUrl,
        e.CategoryGroupId,
        e.CategoryGroup?.Name,
        e.CategoryId,
        e.Category?.Name,
        e.PageCount,
        e.CreatedAt
    );
}
