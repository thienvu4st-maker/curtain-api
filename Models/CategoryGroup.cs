namespace media_app_api.Models;

public class CategoryGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string IconName { get; set; } = string.Empty;

    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
