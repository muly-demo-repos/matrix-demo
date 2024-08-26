using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PassengersControllerBase : ControllerBase
{
    protected readonly IPassengersService _service;

    public PassengersControllerBase(IPassengersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Passenger
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Passenger>> CreatePassenger(PassengerCreateInput input)
    {
        var passenger = await _service.CreatePassenger(input);

        return CreatedAtAction(nameof(Passenger), new { id = passenger.Id }, passenger);
    }

    /// <summary>
    /// Delete one Passenger
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePassenger(
        [FromRoute()] PassengerWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePassenger(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Passengers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Passenger>>> Passengers(
        [FromQuery()] PassengerFindManyArgs filter
    )
    {
        return Ok(await _service.Passengers(filter));
    }

    /// <summary>
    /// Meta data about Passenger records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PassengersMeta(
        [FromQuery()] PassengerFindManyArgs filter
    )
    {
        return Ok(await _service.PassengersMeta(filter));
    }

    /// <summary>
    /// Get one Passenger
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Passenger>> Passenger(
        [FromRoute()] PassengerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Passenger(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Passenger
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePassenger(
        [FromRoute()] PassengerWhereUniqueInput uniqueId,
        [FromQuery()] PassengerUpdateInput passengerUpdateDto
    )
    {
        try
        {
            await _service.UpdatePassenger(uniqueId, passengerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Bookings records to Passenger
    /// </summary>
    [HttpPost("{Id}/bookings")]
    public async Task<ActionResult> ConnectBookings(
        [FromRoute()] PassengerWhereUniqueInput uniqueId,
        [FromQuery()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.ConnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Passenger
    /// </summary>
    [HttpDelete("{Id}/bookings")]
    public async Task<ActionResult> DisconnectBookings(
        [FromRoute()] PassengerWhereUniqueInput uniqueId,
        [FromBody()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.DisconnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Bookings records for Passenger
    /// </summary>
    [HttpGet("{Id}/bookings")]
    public async Task<ActionResult<List<Booking>>> FindBookings(
        [FromRoute()] PassengerWhereUniqueInput uniqueId,
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindBookings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Bookings records for Passenger
    /// </summary>
    [HttpPatch("{Id}/bookings")]
    public async Task<ActionResult> UpdateBookings(
        [FromRoute()] PassengerWhereUniqueInput uniqueId,
        [FromBody()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.UpdateBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
