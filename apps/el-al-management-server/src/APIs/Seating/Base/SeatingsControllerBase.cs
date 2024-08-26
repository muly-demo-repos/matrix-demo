using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SeatingsControllerBase : ControllerBase
{
    protected readonly ISeatingsService _service;

    public SeatingsControllerBase(ISeatingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Seating
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Seating>> CreateSeating(SeatingCreateInput input)
    {
        var seating = await _service.CreateSeating(input);

        return CreatedAtAction(nameof(Seating), new { id = seating.Id }, seating);
    }

    /// <summary>
    /// Delete one Seating
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteSeating([FromRoute()] SeatingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSeating(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Seatings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Seating>>> Seatings(
        [FromQuery()] SeatingFindManyArgs filter
    )
    {
        return Ok(await _service.Seatings(filter));
    }

    /// <summary>
    /// Meta data about Seating records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SeatingsMeta(
        [FromQuery()] SeatingFindManyArgs filter
    )
    {
        return Ok(await _service.SeatingsMeta(filter));
    }

    /// <summary>
    /// Get one Seating
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Seating>> Seating([FromRoute()] SeatingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Seating(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Seating
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSeating(
        [FromRoute()] SeatingWhereUniqueInput uniqueId,
        [FromQuery()] SeatingUpdateInput seatingUpdateDto
    )
    {
        try
        {
            await _service.UpdateSeating(uniqueId, seatingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Booking record for Seating
    /// </summary>
    [HttpGet("{Id}/booking")]
    public async Task<ActionResult<List<Booking>>> GetBooking(
        [FromRoute()] SeatingWhereUniqueInput uniqueId
    )
    {
        var booking = await _service.GetBooking(uniqueId);
        return Ok(booking);
    }
}
