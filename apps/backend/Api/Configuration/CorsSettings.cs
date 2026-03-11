namespace WeatherApp.Backend.Api.Configuration;

public class CorsSettings
{
  public IReadOnlyCollection<string> AllowedOrigins { get; init; } = null!;
}
