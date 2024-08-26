using ElAlManagement.Infrastructure;

namespace ElAlManagement.APIs;

public class FlightsService : FlightsServiceBase
{
    public FlightsService(ElAlManagementDbContext context)
        : base(context) { }
}
