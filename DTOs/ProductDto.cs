namespace media_app_api.DTOs;

public record ProductDto(
    int Id,
    string Title,
    decimal Price,
    string Description,
    string ImageUrl,
    int? CategoryId,
    string CategoryName
);

public record CreateProductDto(
    string Title,
    decimal Price,
    string Description,
    string ImageUrl,
    int? CategoryId
);

public record UpdateProductDto(
    string Title,
    decimal Price,
    string Description,
    string ImageUrl,
    int? CategoryId
);
