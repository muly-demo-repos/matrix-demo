using ElAlManagement.APIs;
using ElAlManagement.APIs.Common;
using ElAlManagement.APIs.Dtos;
using ElAlManagement.APIs.Errors;
using ElAlManagement.APIs.Extensions;
using ElAlManagement.Infrastructure;
using ElAlManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ElAlManagement.APIs;

public abstract class SeatingsServiceBase : ISeatingsService
{
    protected readonly ElAlManagementDbContext _context;

    public SeatingsServiceBase(ElAlManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Seating
    /// </summary>
    public async Task<Seating> CreateSeating(SeatingCreateInput createDto)
    {
        var seating = new SeatingDbModel
        {
            ClassField = createDto.ClassField,
            CreatedAt = createDto.CreatedAt,
            SeatNumber = createDto.SeatNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            seating.Id = createDto.Id;
        }
        if (createDto.Booking != null)
        {
            seating.Booking = await _context
                .Bookings.Where(booking => createDto.Booking.Id == booking.Id)
                .FirstOrDefaultAsync();
        }

        _context.Seatings.Add(seating);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SeatingDbModel>(seating.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Seating
    /// </summary>
    public async Task DeleteSeating(SeatingWhereUniqueInput uniqueId)
    {
        var seating = await _context.Seatings.FindAsync(uniqueId.Id);
        if (seating == null)
        {
            throw new NotFoundException();
        }

        _context.Seatings.Remove(seating);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Seatings
    /// </summary>
    public async Task<List<Seating>> Seatings(SeatingFindManyArgs findManyArgs)
    {
        var seatings = await _context
            .Seatings.Include(x => x.Booking)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return seatings.ConvertAll(seating => seating.ToDto());
    }

    /// <summary>
    /// Meta data about Seating records
    /// </summary>
    public async Task<MetadataDto> SeatingsMeta(SeatingFindManyArgs findManyArgs)
    {
        var count = await _context.Seatings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Seating
    /// </summary>
    public async Task<Seating> Seating(SeatingWhereUniqueInput uniqueId)
    {
        var seatings = await this.Seatings(
            new SeatingFindManyArgs { Where = new SeatingWhereInput { Id = uniqueId.Id } }
        );
        var seating = seatings.FirstOrDefault();
        if (seating == null)
        {
            throw new NotFoundException();
        }

        return seating;
    }

    /// <summary>
    /// Update one Seating
    /// </summary>
    public async Task UpdateSeating(SeatingWhereUniqueInput uniqueId, SeatingUpdateInput updateDto)
    {
        var seating = updateDto.ToModel(uniqueId);

        _context.Entry(seating).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Seatings.Any(e => e.Id == seating.Id))
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
    /// Get a Booking record for Seating
    /// </summary>
    public async Task<Booking> GetBooking(SeatingWhereUniqueInput uniqueId)
    {
        var seating = await _context
            .Seatings.Where(seating => seating.Id == uniqueId.Id)
            .Include(seating => seating.Booking)
            .FirstOrDefaultAsync();
        if (seating == null)
        {
            throw new NotFoundException();
        }
        return seating.Booking.ToDto();
    }
}
