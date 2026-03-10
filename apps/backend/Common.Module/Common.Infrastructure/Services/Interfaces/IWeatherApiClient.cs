using Common.Infrastructure.Models.WeatherApi;

namespace Common.Infrastructure.Services.Interfaces;

/// <summary>
/// Client for interacting with the external Weather API (RapidAPI)
/// </summary>
public interface IWeatherApiClient
{
  /// <summary>
  /// Gets timezone information for a given city
  /// </summary>
  /// <param name="city">City name or coordinates</param>
  /// <returns>Timezone information</returns>
  Task<TimezoneResponse> GetTimezoneAsync(string city);

  /// <summary>
  /// Gets current weather information for a given city
  /// </summary>
  /// <param name="city">City name or coordinates</param>
  /// <returns>Current weather data</returns>
  Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string city);

  /// <summary>
  /// Gets astronomy information for a given city
  /// </summary>
  /// <param name="city">City name or coordinates</param>
  /// <returns>Astronomy data including sunrise, sunset, moon phase, etc.</returns>
  Task<AstronomyResponse> GetAstronomyAsync(string city);
}

