using media_app_api.DTOs;

namespace media_app_api.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? categoryId = null);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto request);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto request);
    Task<bool> DeleteProductAsync(int id);
}
