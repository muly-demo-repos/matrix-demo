namespace ElAlManagement.APIs.Dtos;

public class BookingWhereInput
{
    public DateTime? BookingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Flight { get; set; }

    public string? Id { get; set; }

    public string? Passenger { get; set; }

    public List<string>? Seatings { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
