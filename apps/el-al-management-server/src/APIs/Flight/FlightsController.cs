using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[ApiController()]
public class FlightsController : FlightsControllerBase
{
    public FlightsController(IFlightsService service)
        : base(service) { }
}
