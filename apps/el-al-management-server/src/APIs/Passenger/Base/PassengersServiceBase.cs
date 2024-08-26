using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using ElAlManagement.APIs.Extensions;
using ElAlManagement.Infrastructure;
using ElAlManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ElAlManagement.APIs;

public abstract class PassengersServiceBase : IPassengersService
{
    protected readonly ElAlManagementDbContext _context;

    public PassengersServiceBase(ElAlManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Passenger
    /// </summary>
    public async Task<Passenger> CreatePassenger(PassengerCreateInput createDto)
    {
        var passenger = new PassengerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            PhoneNumber = createDto.PhoneNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            passenger.Id = createDto.Id;
        }
        if (createDto.Bookings != null)
        {
            passenger.Bookings = await _context
                .Bookings.Where(booking =>
                    createDto.Bookings.Select(t => t.Id).Contains(booking.Id)
                )
                .ToListAsync();
        }

        _context.Passengers.Add(passenger);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PassengerDbModel>(passenger.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Passenger
    /// </summary>
    public async Task DeletePassenger(PassengerWhereUniqueInput uniqueId)
    {
        var passenger = await _context.Passengers.FindAsync(uniqueId.Id);
        if (passenger == null)
        {
            throw new NotFoundException();
        }

        _context.Passengers.Remove(passenger);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Passengers
    /// </summary>
    public async Task<List<Passenger>> Passengers(PassengerFindManyArgs findManyArgs)
    {
        var passengers = await _context
            .Passengers.Include(x => x.Bookings)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return passengers.ConvertAll(passenger => passenger.ToDto());
    }

    /// <summary>
    /// Meta data about Passenger records
    /// </summary>
    public async Task<MetadataDto> PassengersMeta(PassengerFindManyArgs findManyArgs)
    {
        var count = await _context.Passengers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Passenger
    /// </summary>
    public async Task<Passenger> Passenger(PassengerWhereUniqueInput uniqueId)
    {
        var passengers = await this.Passengers(
            new PassengerFindManyArgs { Where = new PassengerWhereInput { Id = uniqueId.Id } }
        );
        var passenger = passengers.FirstOrDefault();
        if (passenger == null)
        {
            throw new NotFoundException();
        }

        return passenger;
    }

    /// <summary>
    /// Update one Passenger
    /// </summary>
    public async Task UpdatePassenger(
        PassengerWhereUniqueInput uniqueId,
        PassengerUpdateInput updateDto
    )
    {
        var passenger = updateDto.ToModel(uniqueId);

        if (updateDto.Bookings != null)
        {
            passenger.Bookings = await _context
                .Bookings.Where(booking => updateDto.Bookings.Select(t => t).Contains(booking.Id))
                .ToListAsync();
        }

        _context.Entry(passenger).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Passengers.Any(e => e.Id == passenger.Id))
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
    /// Connect multiple Bookings records to Passenger
    /// </summary>
    public async Task ConnectBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Passengers.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Bookings);

        foreach (var child in childrenToConnect)
        {
            parent.Bookings.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Passenger
    /// </summary>
    public async Task DisconnectBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Passengers.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Bookings?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Bookings records for Passenger
    /// </summary>
    public async Task<List<Booking>> FindBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingFindManyArgs passengerFindManyArgs
    )
    {
        var bookings = await _context
            .Bookings.Where(m => m.PassengerId == uniqueId.Id)
            .ApplyWhere(passengerFindManyArgs.Where)
            .ApplySkip(passengerFindManyArgs.Skip)
            .ApplyTake(passengerFindManyArgs.Take)
            .ApplyOrderBy(passengerFindManyArgs.SortBy)
            .ToListAsync();

        return bookings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Bookings records for Passenger
    /// </summary>
    public async Task UpdateBookings(
        PassengerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var passenger = await _context
            .Passengers.Include(t => t.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (passenger == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        passenger.Bookings = children;
        await _context.SaveChangesAsync();
    }
}
