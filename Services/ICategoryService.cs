using media_app_api.DTOs;

namespace media_app_api.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto request);
    Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto request);
    Task<bool> DeleteCategoryAsync(int id);
}
