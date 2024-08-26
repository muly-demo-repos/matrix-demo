namespace ElAlManagement.APIs.Dtos;

public class FlightCreateInput
{
    public DateTime? ArrivalTime { get; set; }

    public List<Booking>? Bookings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string? Destination { get; set; }

    public string? FlightNumber { get; set; }

    public string? Id { get; set; }

    public string? Origin { get; set; }

    public double? Price { get; set; }

    public DateTime UpdatedAt { get; set; }
}
