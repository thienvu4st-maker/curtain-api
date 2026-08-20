namespace media_app_api.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    string Description,
    string IconName,
    int? ParentId,
    string? ParentName,
    int ProductCount,
    IEnumerable<CategoryDto> SubCategories
);

public record CreateCategoryDto(
    string Name,
    string Description,
    string IconName,
    int? ParentId
);

public record UpdateCategoryDto(
    string Name,
    string Description,
    string IconName,
    int? ParentId
);
