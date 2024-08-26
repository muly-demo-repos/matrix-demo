using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElAlManagement.Infrastructure.Models;

[Table("Seatings")]
public class SeatingDbModel
{
    public string? BookingId { get; set; }

    [ForeignKey(nameof(BookingId))]
    public BookingDbModel? Booking { get; set; } = null;

    [StringLength(1000)]
    public string? ClassField { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? SeatNumber { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
