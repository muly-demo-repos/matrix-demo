namespace ElAlManagement.APIs.Dtos;

public class BookingCreateInput
{
    public DateTime? BookingDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Flight? Flight { get; set; }

    public string? Id { get; set; }

    public Passenger? Passenger { get; set; }

    public List<Seating>? Seatings { get; set; }

    public DateTime UpdatedAt { get; set; }
}
