using Common.Infrastructure.Configuration;
using Common.Infrastructure.Models.WeatherApi;
using Common.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Common.Infrastructure.Services.Implementations;

/// <summary>
/// Decorator that adds in-memory caching to an <see cref="IWeatherApiClient"/> implementation.
/// Caches responses per city with configurable TTLs to reduce external API calls.
/// </summary>
public class CachedWeatherApiClient : IWeatherApiClient
{
  private const string CurrentWeatherPrefix = "weatherapi:current";
  private const string TimezonePrefix = "weatherapi:timezone";
  private const string AstronomyPrefix = "weatherapi:astronomy";

  private readonly IWeatherApiClient _innerClient;
  private readonly IMemoryCache _cache;
  private readonly WeatherApiCacheOptions _options;

  public CachedWeatherApiClient(
    IWeatherApiClient innerClient,
    IMemoryCache cache,
    WeatherApiCacheOptions options)
  {
    _innerClient = innerClient ?? throw new ArgumentNullException(nameof(innerClient));
    _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    _options = options ?? throw new ArgumentNullException(nameof(options));
  }

  public async Task<TimezoneResponse> GetTimezoneAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }

    var cacheKey = BuildCacheKey(TimezonePrefix, city);

    var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
      entry.AbsoluteExpirationRelativeToNow = _options.TimezoneTtl;
      return await _innerClient.GetTimezoneAsync(city);
    });

    return result!;
  }

  public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }

    var cacheKey = BuildCacheKey(CurrentWeatherPrefix, city);

    var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
      entry.AbsoluteExpirationRelativeToNow = _options.CurrentWeatherTtl;
      return await _innerClient.GetCurrentWeatherAsync(city);
    });

    return result!;
  }

  public async Task<AstronomyResponse> GetAstronomyAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }

    var cacheKey = BuildCacheKey(AstronomyPrefix, city);

    var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
      entry.AbsoluteExpirationRelativeToNow = _options.AstronomyTtl;
      return await _innerClient.GetAstronomyAsync(city);
    });

    return result!;
  }

  private static string BuildCacheKey(string prefix, string city)
    => $"{prefix}:{city.Trim().ToLowerInvariant()}";
}

