using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace media_app_api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string IconName { get; set; } = "curtain";

    public int? CategoryGroupId { get; set; }
    public CategoryGroup? CategoryGroup { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
