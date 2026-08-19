using media_app_api.DTOs;

namespace media_app_api.Services;

public interface IBookingService
{
    Task<PagedBookingResultDto> GetBookingsPagedAsync(string? status = null, int pageIndex = 1, int pageSize = 10);
    Task<BookingDto?> GetBookingByIdAsync(int id);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto request);
    Task<BookingDto?> UpdateBookingStatusAsync(int id, string status);
    Task<bool> DeleteBookingAsync(int id);
}
