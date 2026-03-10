using Common.Infrastructure.Dtos;
using Common.Infrastructure.Models.WeatherApi;

namespace Astronomy.Application.Dtos;

public record AstronomyDto
{
  public LocationDto? Location { get; init; }
  public AstronomyDataDto? Astronomy { get; init; }

  public static AstronomyDto MapFrom(AstronomyResponse source)
  {
    return new AstronomyDto
    {
      Location = LocationDto.MapFrom(source.Location),
      Astronomy = AstronomyDataDto.MapFrom(source.Astronomy)
    };
  }
}

public record AstronomyDataDto
{
  public AstroDto? Astro { get; init; }

  public static AstronomyDataDto? MapFrom(AstronomyData? source)
  {
    if (source == null) return null;

    return new AstronomyDataDto
    {
      Astro = AstroDto.MapFrom(source.Astro)
    };
  }
}

public record AstroDto
{
  public string? Sunrise { get; init; }
  public string? Sunset { get; init; }
  public string? Moonrise { get; init; }
  public string? Moonset { get; init; }
  public string? MoonPhase { get; init; }
  public int MoonIllumination { get; init; }
  public bool IsMoonUp { get; init; }
  public bool IsSunUp { get; init; }

  public static AstroDto? MapFrom(Astro? source)
  {
    if (source == null) return null;

    return new AstroDto
    {
      Sunrise = source.Sunrise,
      Sunset = source.Sunset,
      Moonrise = source.Moonrise,
      Moonset = source.Moonset,
      MoonPhase = source.MoonPhase,
      MoonIllumination = source.MoonIllumination,
      IsMoonUp = source.IsMoonUp == 1,
      IsSunUp = source.IsSunUp == 1
    };
  }
}
