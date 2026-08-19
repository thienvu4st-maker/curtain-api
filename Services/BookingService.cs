using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class BookingService(AppDbContext dbContext) : IBookingService
{
    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await dbContext.Bookings
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(b => new BookingDto(
            b.Id,
            b.CustomerName,
            b.PhoneNumber,
            b.Address,
            b.ServiceType,
            b.Notes,
            b.Status,
            b.CreatedAt
        ));
    }

    public async Task<BookingDto?> GetBookingByIdAsync(int id)
    {
        var b = await dbContext.Bookings.FindAsync(id);
        if (b is null) return null;

        return new BookingDto(
            b.Id,
            b.CustomerName,
            b.PhoneNumber,
            b.Address,
            b.ServiceType,
            b.Notes,
            b.Status,
            b.CreatedAt
        );
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto request)
    {
        var booking = new Booking
        {
            CustomerName = request.CustomerName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            ServiceType = request.ServiceType,
            Notes = request.Notes,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Bookings.Add(booking);
        await dbContext.SaveChangesAsync();

        return new BookingDto(
            booking.Id,
            booking.CustomerName,
            booking.PhoneNumber,
            booking.Address,
            booking.ServiceType,
            booking.Notes,
            booking.Status,
            booking.CreatedAt
        );
    }

    public async Task<BookingDto?> UpdateBookingStatusAsync(int id, string status)
    {
        var booking = await dbContext.Bookings.FindAsync(id);
        if (booking is null) return null;

        booking.Status = status;
        await dbContext.SaveChangesAsync();

        return new BookingDto(
            booking.Id,
            booking.CustomerName,
            booking.PhoneNumber,
            booking.Address,
            booking.ServiceType,
            booking.Notes,
            booking.Status,
            booking.CreatedAt
        );
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var booking = await dbContext.Bookings.FindAsync(id);
        if (booking is null) return false;

        dbContext.Bookings.Remove(booking);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
