using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using ElAlManagement.APIs.Extensions;
using ElAlManagement.Infrastructure;
using ElAlManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ElAlManagement.APIs;

public abstract class FlightsServiceBase : IFlightsService
{
    protected readonly ElAlManagementDbContext _context;

    public FlightsServiceBase(ElAlManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Flight
    /// </summary>
    public async Task<Flight> CreateFlight(FlightCreateInput createDto)
    {
        var flight = new FlightDbModel
        {
            ArrivalTime = createDto.ArrivalTime,
            CreatedAt = createDto.CreatedAt,
            DepartureTime = createDto.DepartureTime,
            Destination = createDto.Destination,
            FlightNumber = createDto.FlightNumber,
            Origin = createDto.Origin,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            flight.Id = createDto.Id;
        }
        if (createDto.Bookings != null)
        {
            flight.Bookings = await _context
                .Bookings.Where(booking =>
                    createDto.Bookings.Select(t => t.Id).Contains(booking.Id)
                )
                .ToListAsync();
        }

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FlightDbModel>(flight.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public async Task DeleteFlight(FlightWhereUniqueInput uniqueId)
    {
        var flight = await _context.Flights.FindAsync(uniqueId.Id);
        if (flight == null)
        {
            throw new NotFoundException();
        }

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Flights
    /// </summary>
    public async Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs)
    {
        var flights = await _context
            .Flights.Include(x => x.Bookings)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return flights.ConvertAll(flight => flight.ToDto());
    }

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public async Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs)
    {
        var count = await _context.Flights.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Flight
    /// </summary>
    public async Task<Flight> Flight(FlightWhereUniqueInput uniqueId)
    {
        var flights = await this.Flights(
            new FlightFindManyArgs { Where = new FlightWhereInput { Id = uniqueId.Id } }
        );
        var flight = flights.FirstOrDefault();
        if (flight == null)
        {
            throw new NotFoundException();
        }

        return flight;
    }

    /// <summary>
    /// Update one Flight
    /// </summary>
    public async Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto)
    {
        var flight = updateDto.ToModel(uniqueId);

        if (updateDto.Bookings != null)
        {
            flight.Bookings = await _context
                .Bookings.Where(booking => updateDto.Bookings.Select(t => t).Contains(booking.Id))
                .ToListAsync();
        }

        _context.Entry(flight).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Flights.Any(e => e.Id == flight.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    public async Task ConnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var bookings = await _context
            .Bookings.Where(t => bookingsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (bookings.Count == 0)
        {
            throw new NotFoundException();
        }

        var bookingsToConnect = bookings.Except(parent.Bookings);

        foreach (var booking in bookingsToConnect)
        {
            parent.Bookings.Add(booking);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    public async Task DisconnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var bookings = await _context
            .Bookings.Where(t => bookingsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var booking in bookings)
        {
            parent.Bookings?.Remove(booking);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Bookings records for Flight
    /// </summary>
    public async Task<List<Booking>> FindBookings(
        FlightWhereUniqueInput uniqueId,
        BookingFindManyArgs flightFindManyArgs
    )
    {
        var bookings = await _context
            .Bookings.Where(m => m.FlightId == uniqueId.Id)
            .ApplyWhere(flightFindManyArgs.Where)
            .ApplySkip(flightFindManyArgs.Skip)
            .ApplyTake(flightFindManyArgs.Take)
            .ApplyOrderBy(flightFindManyArgs.SortBy)
            .ToListAsync();

        return bookings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Bookings records for Flight
    /// </summary>
    public async Task UpdateBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    )
    {
        var flight = await _context
            .Flights.Include(t => t.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (flight == null)
        {
            throw new NotFoundException();
        }

        var bookings = await _context
            .Bookings.Where(a => bookingsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (bookings.Count == 0)
        {
            throw new NotFoundException();
        }

        flight.Bookings = bookings;
        await _context.SaveChangesAsync();
    }
}
