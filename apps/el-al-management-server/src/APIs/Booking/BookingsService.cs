using ElAlManagement.Infrastructure;

namespace ElAlManagement.APIs;

public class BookingsService : BookingsServiceBase
{
    public BookingsService(ElAlManagementDbContext context)
        : base(context) { }
}
