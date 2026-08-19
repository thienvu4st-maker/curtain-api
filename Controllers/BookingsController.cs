using media_app_api.DTOs;
using media_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace media_app_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await bookingService.GetBookingByIdAsync(id);
        if (booking is null)
            return NotFound(new { message = $"Booking with id {id} not found." });

        return Ok(booking);
    }

    // Public endpoint for customer web submissions
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerName) || string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return BadRequest(new { message = "Vui lòng nhập họ tên và số điện thoại." });
        }

        var booking = await bookingService.CreateBookingAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [Authorize]
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBookingStatusDto request)
    {
        var booking = await bookingService.UpdateBookingStatusAsync(id, request.Status);
        if (booking is null)
            return NotFound(new { message = $"Booking with id {id} not found." });

        return Ok(booking);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await bookingService.DeleteBookingAsync(id);
        if (!success)
            return NotFound(new { message = $"Booking with id {id} not found." });

        return NoContent();
    }
}
