using System.Net;
using System.Text.Json;
using Common.Infrastructure.Exceptions;
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

    await EnsureSuccessResponse(response, city);

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<TimezoneResponse>(content);

    return result ?? throw new ApplicationLogicException("Failed to retrieve timezone data. Please try again later.");
  }

  public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }

    var url = $"{_baseUrl}/current.json?q={Uri.EscapeDataString(city)}";
    var response = await _httpClient.GetAsync(url);

    await EnsureSuccessResponse(response, city);

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<CurrentWeatherResponse>(content);

    return result ?? throw new ApplicationLogicException("Failed to retrieve weather data. Please try again later.");
  }

  public async Task<AstronomyResponse> GetAstronomyAsync(string city)
  {
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City cannot be null or empty.", nameof(city));
    }


    var url = $"{_baseUrl}/astronomy.json?q={Uri.EscapeDataString(city)}";
    var response = await _httpClient.GetAsync(url);

    await EnsureSuccessResponse(response, city);

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<AstronomyResponse>(content);

    return result ?? throw new ApplicationLogicException("Failed to retrieve astronomy data. Please try again later.");
  }

  private static async Task EnsureSuccessResponse(HttpResponseMessage response, string city)
  {
    if (response.IsSuccessStatusCode)
    {
      return;
    }

    // Read error content to ensure the response stream is consumed
    await response.Content.ReadAsStringAsync();

    switch (response.StatusCode)
    {
      case HttpStatusCode.NotFound:
        throw new NotFoundException($"No data found for city '{city}'.");
      case HttpStatusCode.BadRequest:
        throw new ApplicationLogicException(
          $"Unable to process the request for city '{city}'. Please check the city name and try again.");
      default:
        throw new ApplicationLogicException(
          $"An error occurred while fetching data for city '{city}'. Please try again later.");
    }
  }
}

