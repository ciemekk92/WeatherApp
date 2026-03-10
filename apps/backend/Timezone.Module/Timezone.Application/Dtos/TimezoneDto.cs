using Common.Infrastructure.Dtos;
using Common.Infrastructure.Models.WeatherApi;

namespace Timezone.Application.Dtos;

public record TimezoneDto
{
  public LocationDto? Location { get; init; }

  public static TimezoneDto MapFrom(TimezoneResponse source)
    => new()
    {
      Location = LocationDto.MapFrom(source.Location)
    };
}
