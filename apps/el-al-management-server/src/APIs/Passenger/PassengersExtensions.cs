using ElAlManagement.APIs.Dtos;
using ElAlManagement.Infrastructure.Models;

namespace ElAlManagement.APIs.Extensions;

public static class PassengersExtensions
{
    public static Passenger ToDto(this PassengerDbModel model)
    {
        return new Passenger
        {
            Bookings = model.Bookings?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PassengerDbModel ToModel(
        this PassengerUpdateInput updateDto,
        PassengerWhereUniqueInput uniqueId
    )
    {
        var passenger = new PassengerDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            PhoneNumber = updateDto.PhoneNumber
        };

        if (updateDto.CreatedAt != null)
        {
            passenger.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            passenger.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return passenger;
    }
}
