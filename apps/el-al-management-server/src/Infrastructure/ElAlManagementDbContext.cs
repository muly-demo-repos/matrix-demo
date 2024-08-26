using ElAlManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElAlManagement.Infrastructure;

public class ElAlManagementDbContext : IdentityDbContext<IdentityUser>
{
    public ElAlManagementDbContext(DbContextOptions<ElAlManagementDbContext> options)
        : base(options) { }

    public DbSet<FlightDbModel> Flights { get; set; }

    public DbSet<PassengerDbModel> Passengers { get; set; }

    public DbSet<SeatingDbModel> Seatings { get; set; }

    public DbSet<BookingDbModel> Bookings { get; set; }
}
