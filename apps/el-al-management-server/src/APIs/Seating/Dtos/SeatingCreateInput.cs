namespace ElAlManagement.APIs.Dtos;

public class SeatingCreateInput
{
    public Booking? Booking { get; set; }

    public string? ClassField { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? SeatNumber { get; set; }

    public DateTime UpdatedAt { get; set; }
}
