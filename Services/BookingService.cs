using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace media_app_api.Services;

public class BookingService(AppDbContext dbContext) : IBookingService
{
    public async Task<PagedBookingResultDto> GetBookingsPagedAsync(string? status = null, int pageIndex = 1, int pageSize = 10)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var query = dbContext.Bookings.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(b => b.Status.ToLower() == status.ToLower());
        }

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var bookings = await query
            .OrderByDescending(b => b.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = bookings.Select(b => new BookingDto(
            b.Id,
            b.CustomerName,
            b.PhoneNumber,
            b.Address,
            b.ServiceType,
            b.Notes,
            b.Status,
            b.CreatedAt
        ));

        return new PagedBookingResultDto(
            items,
            totalCount,
            pageIndex,
            pageSize,
            totalPages
        );
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
