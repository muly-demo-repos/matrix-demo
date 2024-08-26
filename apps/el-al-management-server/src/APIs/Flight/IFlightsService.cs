using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;

namespace ElAlManagement.APIs;

public interface IFlightsService
{
    /// <summary>
    /// Create one Flight
    /// </summary>
    public Task<Flight> CreateFlight(FlightCreateInput flight);

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public Task DeleteFlight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Flights
    /// </summary>
    public Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Flight
    /// </summary>
    public Task<Flight> Flight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Flight
    /// </summary>
    public Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    public Task ConnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    public Task DisconnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Find multiple Bookings records for Flight
    /// </summary>
    public Task<List<Booking>> FindBookings(
        FlightWhereUniqueInput uniqueId,
        BookingFindManyArgs BookingFindManyArgs
    );

    /// <summary>
    /// Update multiple Bookings records for Flight
    /// </summary>
    public Task UpdateBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );
}
