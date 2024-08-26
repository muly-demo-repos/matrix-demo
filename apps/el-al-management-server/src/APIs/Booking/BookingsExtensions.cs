using ElAlManagement.APIs.Dtos;
using ElAlManagement.Infrastructure.Models;

namespace ElAlManagement.APIs.Extensions;

public static class BookingsExtensions
{
    public static Booking ToDto(this BookingDbModel model)
    {
        return new Booking
        {
            BookingDate = model.BookingDate,
            CreatedAt = model.CreatedAt,
            Flight = model.FlightId,
            Id = model.Id,
            Passenger = model.PassengerId,
            Seatings = model.Seatings?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BookingDbModel ToModel(
        this BookingUpdateInput updateDto,
        BookingWhereUniqueInput uniqueId
    )
    {
        var booking = new BookingDbModel { Id = uniqueId.Id, BookingDate = updateDto.BookingDate };

        if (updateDto.CreatedAt != null)
        {
            booking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Flight != null)
        {
            booking.FlightId = updateDto.Flight;
        }
        if (updateDto.Passenger != null)
        {
            booking.PassengerId = updateDto.Passenger;
        }
        if (updateDto.UpdatedAt != null)
        {
            booking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return booking;
    }
}
