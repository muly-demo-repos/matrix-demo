namespace ElAlManagement.APIs.Dtos;

public class FlightWhereInput
{
    public DateTime? ArrivalTime { get; set; }

    public List<string>? Bookings { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string? Destination { get; set; }

    public string? FlightNumber { get; set; }

    public string? Id { get; set; }

    public string? Origin { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
