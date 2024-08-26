using ElAlManagement.Infrastructure;

namespace ElAlManagement.APIs;

public class PassengersService : PassengersServiceBase
{
    public PassengersService(ElAlManagementDbContext context)
        : base(context) { }
}
