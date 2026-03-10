using System.Text.Json;
using Common.Infrastructure.Models.WeatherApi;
using Common.Infrastructure.Services.Interfaces;

namespace Common.Infrastructure.Services.Implementations;

public class WeatherApiClient : IWeatherApiClient
{
  private readonly HttpClient _httpClient;
  private readonly string _baseUrl;

  public WeatherApiClient(HttpClient httpClient, ISecretsProvider secretsProvider)
  {
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    ArgumentNullException.ThrowIfNull(secretsProvider);

    // Initialize synchronously - secrets should be loaded at startup
    _baseUrl = secretsProvider.GetSecretAsync("WEATHER_API_BASE_URL").GetAwaiter().GetResult()
               ?? throw new InvalidOperationException("WEATHER_API_BASE_URL secret is not configured.");

    var apiKey = secretsProvider.GetSecretAsync("WEATHER_API_KEY").GetAwaiter().GetResult()
                 ?? throw new InvalidOperationException("WEATHER_API_KEY secret is not configured.");

    // Extract host from base URL for RapidAPI header
    var uri = new Uri(_baseUrl);
    var host = uri.Host;

    // Configure HttpClient headers
    _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
    _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", host);
  }

  public async Task<TimezoneResponse> GetTimezoneAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }


    var url = $"{_baseUrl}/timezone.json?q={Uri.EscapeDataString(city)}";
    var response = await _httpClient.GetAsync(url);

    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<TimezoneResponse>(content);

    return result ?? throw new InvalidOperationException("Failed to deserialize timezone response.");
  }

  public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }


    var url = $"{_baseUrl}/current.json?q={Uri.EscapeDataString(city)}";
    var response = await _httpClient.GetAsync(url);

    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<CurrentWeatherResponse>(content);

    return result ?? throw new InvalidOperationException("Failed to deserialize current weather response.");
  }

  public async Task<AstronomyResponse> GetAstronomyAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }


    var url = $"{_baseUrl}/astronomy.json?q={Uri.EscapeDataString(city)}";
    var response = await _httpClient.GetAsync(url);

    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<AstronomyResponse>(content);

    return result ?? throw new InvalidOperationException("Failed to deserialize astronomy response.");
  }
}

