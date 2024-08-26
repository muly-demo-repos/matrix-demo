using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FlightsControllerBase : ControllerBase
{
    protected readonly IFlightsService _service;

    public FlightsControllerBase(IFlightsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Flight
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Flight>> CreateFlight(FlightCreateInput input)
    {
        var flight = await _service.CreateFlight(input);

        return CreatedAtAction(nameof(Flight), new { id = flight.Id }, flight);
    }

    /// <summary>
    /// Delete one Flight
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteFlight([FromRoute()] FlightWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteFlight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Flights
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Flight>>> Flights([FromQuery()] FlightFindManyArgs filter)
    {
        return Ok(await _service.Flights(filter));
    }

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FlightsMeta(
        [FromQuery()] FlightFindManyArgs filter
    )
    {
        return Ok(await _service.FlightsMeta(filter));
    }

    /// <summary>
    /// Get one Flight
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Flight>> Flight([FromRoute()] FlightWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Flight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Flight
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateFlight(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] FlightUpdateInput flightUpdateDto
    )
    {
        try
        {
            await _service.UpdateFlight(uniqueId, flightUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    [HttpPost("{Id}/bookings")]
    public async Task<ActionResult> ConnectBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    [HttpDelete("{Id}/bookings")]
    public async Task<ActionResult> DisconnectBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
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
    /// Find multiple Bookings records for Flight
    /// </summary>
    [HttpGet("{Id}/bookings")]
    public async Task<ActionResult<List<Booking>>> FindBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
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
    /// Update multiple Bookings records for Flight
    /// </summary>
    [HttpPatch("{Id}/bookings")]
    public async Task<ActionResult> UpdateBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
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
