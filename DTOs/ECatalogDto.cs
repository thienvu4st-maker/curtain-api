namespace media_app_api.DTOs;

public record ECatalogDto(
    int Id,
    string Title,
    string Description,
    string CoverImageUrl,
    string PdfUrl,
    int? CategoryGroupId,
    string? CategoryGroupName,
    int? CategoryId,
    string? CategoryName,
    int PageCount,
    DateTime CreatedAt
);

public record CreateECatalogDto(
    string Title,
    string Description,
    string CoverImageUrl,
    string PdfUrl,
    int? CategoryGroupId,
    int? CategoryId,
    int PageCount
);

public record UpdateECatalogDto(
    string Title,
    string Description,
    string CoverImageUrl,
    string PdfUrl,
    int? CategoryGroupId,
    int? CategoryId,
    int PageCount
);
