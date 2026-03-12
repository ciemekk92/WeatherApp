using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timezone.Application.Features.Queries.GetTimezone;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimezoneController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  public async Task<IActionResult> GetTimezone(string city)
    => Ok(await dispatcher.Send(new GetTimezoneQuery(city)));
}

