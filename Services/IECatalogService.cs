using media_app_api.DTOs;

namespace media_app_api.Services;

public interface IECatalogService
{
    Task<IEnumerable<ECatalogDto>> GetECatalogsAsync(int? categoryGroupId = null, int? categoryId = null);
    Task<ECatalogDto?> GetECatalogByIdAsync(int id);
    Task<ECatalogDto> CreateECatalogAsync(CreateECatalogDto dto);
    Task<ECatalogDto?> UpdateECatalogAsync(int id, UpdateECatalogDto dto);
    Task<bool> DeleteECatalogAsync(int id);
}
