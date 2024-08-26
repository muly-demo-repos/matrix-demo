using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElAlManagement.Infrastructure.Models;

[Table("Flights")]
public class FlightDbModel
{
    public DateTime? ArrivalTime { get; set; }

    public List<BookingDbModel>? Bookings { get; set; } = new List<BookingDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    [StringLength(1000)]
    public string? Destination { get; set; }

    [StringLength(1000)]
    public string? FlightNumber { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Origin { get; set; }

    [Range(100, 200)]
    public double? Price { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
