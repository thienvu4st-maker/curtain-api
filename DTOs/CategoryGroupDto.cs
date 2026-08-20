namespace media_app_api.DTOs;

public record CategoryGroupDto(
    int Id,
    string Name,
    string Description,
    string IconName,
    IEnumerable<CategoryDto> Categories
);

public record CreateCategoryGroupDto(
    string Name,
    string Description,
    string IconName
);

public record UpdateCategoryGroupDto(
    string Name,
    string Description,
    string IconName
);
