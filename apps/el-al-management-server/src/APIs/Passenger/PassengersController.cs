using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[ApiController()]
public class PassengersController : PassengersControllerBase
{
    public PassengersController(IPassengersService service)
        : base(service) { }
}
