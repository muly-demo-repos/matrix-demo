using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;

namespace ElAlManagement.APIs;

public interface IPassengersService
{
    /// <summary>
    /// Create one Passenger
    /// </summary>
    public Task<Passenger> CreatePassenger(PassengerCreateInput passenger);

    /// <summary>
    /// Delete one Passenger
    /// </summary>
    public Task DeletePassenger(PassengerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Passengers
    /// </summary>
    public Task<List<Passenger>> Passengers(PassengerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Passenger records
    /// </summary>
    public Task<MetadataDto> PassengersMeta(PassengerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Passenger
    /// </summary>
    public Task<Passenger> Passenger(PassengerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Passenger
    /// </summary>
    public Task UpdatePassenger(PassengerWhereUniqueInput uniqueId, PassengerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Bookings records to Passenger
    /// </summary>
    public Task ConnectBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Disconnect multiple Bookings records from Passenger
    /// </summary>
    public Task DisconnectBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Find multiple Bookings records for Passenger
    /// </summary>
    public Task<List<Booking>> FindBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingFindManyArgs BookingFindManyArgs
    );

    /// <summary>
    /// Update multiple Bookings records for Passenger
    /// </summary>
    public Task UpdateBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );
}
