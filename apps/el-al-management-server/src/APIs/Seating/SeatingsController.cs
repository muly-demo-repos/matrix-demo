using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[ApiController()]
public class SeatingsController : SeatingsControllerBase
{
    public SeatingsController(ISeatingsService service)
        : base(service) { }
}
