namespace WeatherApp.Backend.Api.Configuration;

public class CacheSettings
{
  public int CurrentWeatherTtlMinutes { get; init; } = 1;
  public int TimezoneTtlMinutes { get; init; } = 60;
  public int AstronomyTtlMinutes { get; init; } = 60;
}

