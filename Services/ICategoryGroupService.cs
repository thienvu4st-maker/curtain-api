using media_app_api.DTOs;

namespace media_app_api.Services;

public interface ICategoryGroupService
{
    Task<IEnumerable<CategoryGroupDto>> GetCategoryGroupsAsync();
    Task<CategoryGroupDto?> GetCategoryGroupByIdAsync(int id);
    Task<CategoryGroupDto> CreateCategoryGroupAsync(CreateCategoryGroupDto request);
    Task<CategoryGroupDto?> UpdateCategoryGroupAsync(int id, UpdateCategoryGroupDto request);
    Task<bool> DeleteCategoryGroupAsync(int id);
}
