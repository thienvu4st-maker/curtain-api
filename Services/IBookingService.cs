using media_app_api.DTOs;

namespace media_app_api.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<BookingDto?> GetBookingByIdAsync(int id);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto request);
    Task<BookingDto?> UpdateBookingStatusAsync(int id, string status);
    Task<bool> DeleteBookingAsync(int id);
}
