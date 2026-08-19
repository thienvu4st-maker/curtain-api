using System.ComponentModel.DataAnnotations;

namespace media_app_api.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]
    public string ServiceType { get; set; } = "Thi công Rèm Cửa";

    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
