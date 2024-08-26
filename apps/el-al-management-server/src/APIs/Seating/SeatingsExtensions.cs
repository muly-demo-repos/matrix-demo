using ElAlManagement.APIs.Dtos;
using ElAlManagement.Infrastructure.Models;

namespace ElAlManagement.APIs.Extensions;

public static class SeatingsExtensions
{
    public static Seating ToDto(this SeatingDbModel model)
    {
        return new Seating
        {
            Booking = model.BookingId,
            ClassField = model.ClassField,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            SeatNumber = model.SeatNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SeatingDbModel ToModel(
        this SeatingUpdateInput updateDto,
        SeatingWhereUniqueInput uniqueId
    )
    {
        var seating = new SeatingDbModel
        {
            Id = uniqueId.Id,
            ClassField = updateDto.ClassField,
            SeatNumber = updateDto.SeatNumber
        };

        if (updateDto.Booking != null)
        {
            seating.BookingId = updateDto.Booking;
        }
        if (updateDto.CreatedAt != null)
        {
            seating.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            seating.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return seating;
    }
}
