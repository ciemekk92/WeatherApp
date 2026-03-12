namespace Common.Infrastructure.Configuration;

public class WeatherApiCacheOptions
{
  public TimeSpan CurrentWeatherTtl { get; init; } = TimeSpan.FromMinutes(1);
  public TimeSpan TimezoneTtl { get; init; } = TimeSpan.FromMinutes(60);
  public TimeSpan AstronomyTtl { get; init; } = TimeSpan.FromMinutes(60);
}
