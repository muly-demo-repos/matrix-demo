using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs;

[ApiController()]
public class BookingsController : BookingsControllerBase
{
    public BookingsController(IBookingsService service)
        : base(service) { }
}
