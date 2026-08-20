using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace media_app_api.Models;

[Table("ECatalogs")]
public class ECatalog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string CoverImageUrl { get; set; } = string.Empty;

    [Required]
    public string PdfUrl { get; set; } = string.Empty;

    public int? CategoryGroupId { get; set; }

    [ForeignKey(nameof(CategoryGroupId))]
    public CategoryGroup? CategoryGroup { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    public int PageCount { get; set; } = 1;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
