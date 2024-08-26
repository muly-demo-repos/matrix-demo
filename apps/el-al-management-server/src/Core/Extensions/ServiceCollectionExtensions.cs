using ElAlManagement.APIs;

namespace ElAlManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingsService, BookingsService>();
        services.AddScoped<IFlightsService, FlightsService>();
        services.AddScoped<IPassengersService, PassengersService>();
        services.AddScoped<ISeatingsService, SeatingsService>();
    }
}
