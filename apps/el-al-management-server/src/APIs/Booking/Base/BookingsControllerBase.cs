using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BookingsControllerBase : ControllerBase
{
    protected readonly IBookingsService _service;

    public BookingsControllerBase(IBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Booking
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Booking>> CreateBooking(BookingCreateInput input)
    {
        var booking = await _service.CreateBooking(input);

        return CreatedAtAction(nameof(Booking), new { id = booking.Id }, booking);
    }

    /// <summary>
    /// Delete one Booking
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBooking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bookings
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Booking>>> Bookings(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.Bookings(filter));
    }

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BookingsMeta(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.BookingsMeta(filter));
    }

    /// <summary>
    /// Get one Booking
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Booking>> Booking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Booking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Booking
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBooking(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromQuery()] BookingUpdateInput bookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateBooking(uniqueId, bookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Flight record for Booking
    /// </summary>
    [HttpGet("{Id}/flights")]
    public async Task<ActionResult<List<Flight>>> GetFlight(
        [FromRoute()] BookingWhereUniqueInput uniqueId
    )
    {
        var flight = await _service.GetFlight(uniqueId);
        return Ok(flight);
    }

    /// <summary>
    /// Get a Passenger record for Booking
    /// </summary>
    [HttpGet("{Id}/passengers")]
    public async Task<ActionResult<List<Passenger>>> GetPassenger(
        [FromRoute()] BookingWhereUniqueInput uniqueId
    )
    {
        var passenger = await _service.GetPassenger(uniqueId);
        return Ok(passenger);
    }

    /// <summary>
    /// Connect multiple Seatings records to Booking
    /// </summary>
    [HttpPost("{Id}/seatings")]
    public async Task<ActionResult> ConnectSeatings(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromQuery()] SeatingWhereUniqueInput[] seatingsId
    )
    {
        try
        {
            await _service.ConnectSeatings(uniqueId, seatingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Seatings records from Booking
    /// </summary>
    [HttpDelete("{Id}/seatings")]
    public async Task<ActionResult> DisconnectSeatings(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromBody()] SeatingWhereUniqueInput[] seatingsId
    )
    {
        try
        {
            await _service.DisconnectSeatings(uniqueId, seatingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Seatings records for Booking
    /// </summary>
    [HttpGet("{Id}/seatings")]
    public async Task<ActionResult<List<Seating>>> FindSeatings(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromQuery()] SeatingFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSeatings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Seatings records for Booking
    /// </summary>
    [HttpPatch("{Id}/seatings")]
    public async Task<ActionResult> UpdateSeatings(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromBody()] SeatingWhereUniqueInput[] seatingsId
    )
    {
        try
        {
            await _service.UpdateSeatings(uniqueId, seatingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
