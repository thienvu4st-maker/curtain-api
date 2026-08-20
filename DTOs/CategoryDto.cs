namespace media_app_api.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    string Description,
    string IconName,
    int? CategoryGroupId,
    string? CategoryGroupName,
    int? ParentId,
    string? ParentName,
    int ProductCount,
    IEnumerable<CategoryDto> SubCategories
);

public record CreateCategoryDto(
    string Name,
    string Description,
    string IconName,
    int? CategoryGroupId,
    int? ParentId
);

public record UpdateCategoryDto(
    string Name,
    string Description,
    string IconName,
    int? CategoryGroupId,
    int? ParentId
);
