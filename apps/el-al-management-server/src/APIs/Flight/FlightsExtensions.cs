using ElAlManagement.APIs.Dtos;
using ElAlManagement.Infrastructure.Models;

namespace ElAlManagement.APIs.Extensions;

public static class FlightsExtensions
{
    public static Flight ToDto(this FlightDbModel model)
    {
        return new Flight
        {
            ArrivalTime = model.ArrivalTime,
            Bookings = model.Bookings?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            DepartureTime = model.DepartureTime,
            Destination = model.Destination,
            FlightNumber = model.FlightNumber,
            Id = model.Id,
            Origin = model.Origin,
            Price = model.Price,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FlightDbModel ToModel(
        this FlightUpdateInput updateDto,
        FlightWhereUniqueInput uniqueId
    )
    {
        var flight = new FlightDbModel
        {
            Id = uniqueId.Id,
            ArrivalTime = updateDto.ArrivalTime,
            DepartureTime = updateDto.DepartureTime,
            Destination = updateDto.Destination,
            FlightNumber = updateDto.FlightNumber,
            Origin = updateDto.Origin,
            Price = updateDto.Price
        };

        if (updateDto.CreatedAt != null)
        {
            flight.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            flight.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return flight;
    }
}
