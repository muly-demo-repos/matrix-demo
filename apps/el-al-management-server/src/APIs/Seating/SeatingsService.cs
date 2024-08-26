using ElAlManagement.Infrastructure;

namespace ElAlManagement.APIs;

public class SeatingsService : SeatingsServiceBase
{
    public SeatingsService(ElAlManagementDbContext context)
        : base(context) { }
}
