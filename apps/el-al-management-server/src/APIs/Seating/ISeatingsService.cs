using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;

namespace ElAlManagement.APIs;

public interface ISeatingsService
{
    /// <summary>
    /// Create one Seating
    /// </summary>
    public Task<Seating> CreateSeating(SeatingCreateInput seating);

    /// <summary>
    /// Delete one Seating
    /// </summary>
    public Task DeleteSeating(SeatingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Seatings
    /// </summary>
    public Task<List<Seating>> Seatings(SeatingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Seating records
    /// </summary>
    public Task<MetadataDto> SeatingsMeta(SeatingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Seating
    /// </summary>
    public Task<Seating> Seating(SeatingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Seating
    /// </summary>
    public Task UpdateSeating(SeatingWhereUniqueInput uniqueId, SeatingUpdateInput updateDto);

    /// <summary>
    /// Get a Booking record for Seating
    /// </summary>
    public Task<Booking> GetBooking(SeatingWhereUniqueInput uniqueId);
}
