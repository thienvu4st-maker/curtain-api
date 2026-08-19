namespace media_app_api.DTOs;

public record BookingDto(
    int Id,
    string CustomerName,
    string PhoneNumber,
    string Address,
    string ServiceType,
    string Notes,
    string Status,
    DateTime CreatedAt
);

public record CreateBookingDto(
    string CustomerName,
    string PhoneNumber,
    string Address,
    string ServiceType,
    string Notes
);

public record UpdateBookingStatusDto(
    string Status
);

public record PagedBookingResultDto(
    IEnumerable<BookingDto> Items,
    int TotalCount,
    int PageIndex,
    int PageSize,
    int TotalPages
);
