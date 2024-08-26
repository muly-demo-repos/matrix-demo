using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;

namespace ElAlManagement.APIs;

public interface IBookingsService
{
    /// <summary>
    /// Create one Booking
    /// </summary>
    public Task<Booking> CreateBooking(BookingCreateInput booking);

    /// <summary>
    /// Delete one Booking
    /// </summary>
    public Task DeleteBooking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Bookings
    /// </summary>
    public Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    public Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Booking
    /// </summary>
    public Task<Booking> Booking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Booking
    /// </summary>
    public Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto);

    /// <summary>
    /// Get a Flight record for Booking
    /// </summary>
    public Task<Flight> GetFlight(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Passenger record for Booking
    /// </summary>
    public Task<Passenger> GetPassenger(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Seatings records to Booking
    /// </summary>
    public Task ConnectSeatings(
        BookingWhereUniqueInput uniqueId,
        SeatingWhereUniqueInput[] seatingsId
    );

    /// <summary>
    /// Disconnect multiple Seatings records from Booking
    /// </summary>
    public Task DisconnectSeatings(
        BookingWhereUniqueInput uniqueId,
        SeatingWhereUniqueInput[] seatingsId
    );

    /// <summary>
    /// Find multiple Seatings records for Booking
    /// </summary>
    public Task<List<Seating>> FindSeatings(
        BookingWhereUniqueInput uniqueId,
        SeatingFindManyArgs SeatingFindManyArgs
    );

    /// <summary>
    /// Update multiple Seatings records for Booking
    /// </summary>
    public Task UpdateSeatings(
        BookingWhereUniqueInput uniqueId,
        SeatingWhereUniqueInput[] seatingsId
    );
}
