using ElAlManagement.APIs.Common;
using ElAlManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElAlManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class FlightFindManyArgs : FindManyInput<Flight, FlightWhereInput> { }
