namespace media_app_api.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    string Description,
    string IconName,
    int ProductCount
);

public record CreateCategoryDto(
    string Name,
    string? Description,
    string? IconName
);

public record UpdateCategoryDto(
    string Name,
    string? Description,
    string? IconName
);
