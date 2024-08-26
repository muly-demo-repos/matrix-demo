namespace ElAlManagement.APIs.Dtos;

public class PassengerCreateInput
{
    public List<Booking>? Bookings { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime UpdatedAt { get; set; }
}
