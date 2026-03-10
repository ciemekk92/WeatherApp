using System.Text.Json.Serialization;

namespace Common.Infrastructure.Models.WeatherApi;

public class AstronomyResponse
{
  [JsonPropertyName("location")]
  public LocationInfo? Location { get; set; }

  [JsonPropertyName("astronomy")]
  public AstronomyData? Astronomy { get; set; }
}

public class AstronomyData
{
  [JsonPropertyName("astro")]
  public Astro? Astro { get; set; }
}

public class Astro
{
  [JsonPropertyName("sunrise")]
  public string? Sunrise { get; set; }

  [JsonPropertyName("sunset")]
  public string? Sunset { get; set; }

  [JsonPropertyName("moonrise")]
  public string? Moonrise { get; set; }

  [JsonPropertyName("moonset")]
  public string? Moonset { get; set; }

  [JsonPropertyName("moon_phase")]
  public string? MoonPhase { get; set; }

  [JsonPropertyName("moon_illumination")]
  public int MoonIllumination { get; set; }

  [JsonPropertyName("is_moon_up")]
  public int IsMoonUp { get; set; }

  [JsonPropertyName("is_sun_up")]
  public int IsSunUp { get; set; }
}

