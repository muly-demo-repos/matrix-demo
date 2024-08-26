using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElAlManagement.Infrastructure.Models;

[Table("Bookings")]
public class BookingDbModel
{
    public DateTime? BookingDate { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? FlightId { get; set; }

    [ForeignKey(nameof(FlightId))]
    public FlightDbModel? Flight { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? PassengerId { get; set; }

    [ForeignKey(nameof(PassengerId))]
    public PassengerDbModel? Passenger { get; set; } = null;

    public List<SeatingDbModel>? Seatings { get; set; } = new List<SeatingDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
