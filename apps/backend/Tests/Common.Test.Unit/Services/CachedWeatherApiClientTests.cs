using Common.Infrastructure.Configuration;
using Common.Infrastructure.Models.WeatherApi;
using Common.Infrastructure.Services.Implementations;
using Common.Infrastructure.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace Common.Test.Unit.Services;

public class CachedWeatherApiClientTests : IDisposable
{
  private readonly IWeatherApiClient _innerClientMock;
  private readonly MemoryCache _cache;
  private readonly CachedWeatherApiClient _cachedClient;

  public CachedWeatherApiClientTests()
  {
    _innerClientMock = Substitute.For<IWeatherApiClient>();
    _cache = new MemoryCache(new MemoryCacheOptions());

    var options = new WeatherApiCacheOptions
    {
      CurrentWeatherTtl = TimeSpan.FromMinutes(1),
      TimezoneTtl = TimeSpan.FromMinutes(60),
      AstronomyTtl = TimeSpan.FromMinutes(60)
    };

    _cachedClient = new CachedWeatherApiClient(_innerClientMock, _cache, options);
  }

  public void Dispose()
  {
    _cache.Dispose();
    GC.SuppressFinalize(this);
  }

  [Fact]
  public async Task ShouldReturnCachedCurrentWeatherWhenSameCityIsRequestedTwice()
  {
    // Given
    var expected = new CurrentWeatherResponse { Location = new LocationInfo { Name = "London" } };
    _innerClientMock.GetCurrentWeatherAsync("London").Returns(expected);

    // When
    var first = await _cachedClient.GetCurrentWeatherAsync("London");
    var second = await _cachedClient.GetCurrentWeatherAsync("London");

    // Then
    first.Should().BeSameAs(expected);
    second.Should().BeSameAs(expected);
    await _innerClientMock.Received(1).GetCurrentWeatherAsync("London");
  }

  [Theory]
  [InlineData(" London ", "london")]
  [InlineData("LONDON", "london")]
  [InlineData("  london  ", "London")]
  public async Task ShouldReturnCachedCurrentWeatherWhenCityVariantsDifferByCase(string firstCity, string secondCity)
  {
    // Given
    var expected = new CurrentWeatherResponse { Location = new LocationInfo { Name = "London" } };
    _innerClientMock.GetCurrentWeatherAsync(Arg.Any<string>()).Returns(expected);

    // When
    var first = await _cachedClient.GetCurrentWeatherAsync(firstCity);
    var second = await _cachedClient.GetCurrentWeatherAsync(secondCity);

    // Then
    first.Should().BeSameAs(expected);
    second.Should().BeSameAs(expected);
    await _innerClientMock.Received(1).GetCurrentWeatherAsync(Arg.Any<string>());
  }

  [Fact]
  public async Task ShouldReturnSeparateCurrentWeatherWhenDifferentCitiesAreRequested()
  {
    // Given
    var londonResponse = new CurrentWeatherResponse { Location = new LocationInfo { Name = "London" } };
    var parisResponse = new CurrentWeatherResponse { Location = new LocationInfo { Name = "Paris" } };

    _innerClientMock.GetCurrentWeatherAsync("London").Returns(londonResponse);
    _innerClientMock.GetCurrentWeatherAsync("Paris").Returns(parisResponse);

    // When
    var london = await _cachedClient.GetCurrentWeatherAsync("London");
    var paris = await _cachedClient.GetCurrentWeatherAsync("Paris");

    // Then
    london.Should().BeSameAs(londonResponse);
    paris.Should().BeSameAs(parisResponse);
    await _innerClientMock.Received(1).GetCurrentWeatherAsync("London");
    await _innerClientMock.Received(1).GetCurrentWeatherAsync("Paris");
  }

  [Fact]
  public async Task ShouldReturnCachedTimezoneWhenSameCityIsRequestedTwice()
  {
    // Given
    var expected = new TimezoneResponse { Location = new LocationInfo { Name = "London" } };
    _innerClientMock.GetTimezoneAsync("London").Returns(expected);

    // When
    var first = await _cachedClient.GetTimezoneAsync("London");
    var second = await _cachedClient.GetTimezoneAsync("London");

    // Then
    first.Should().BeSameAs(expected);
    second.Should().BeSameAs(expected);
    await _innerClientMock.Received(1).GetTimezoneAsync("London");
  }

  [Fact]
  public async Task ShouldReturnCachedAstronomyWhenSameCityIsRequestedTwice()
  {
    // Given
    var expected = new AstronomyResponse { Location = new LocationInfo { Name = "London" } };
    _innerClientMock.GetAstronomyAsync("London").Returns(expected);

    // When
    var first = await _cachedClient.GetAstronomyAsync("London");
    var second = await _cachedClient.GetAstronomyAsync("London");

    // Then
    first.Should().BeSameAs(expected);
    second.Should().BeSameAs(expected);
    await _innerClientMock.Received(1).GetAstronomyAsync("London");
  }

  [Fact]
  public async Task ShouldCallInnerClientForEachEndpointWhenSameCityIsRequestedAcrossEndpoints()
  {
    // Given
    var weatherResponse = new CurrentWeatherResponse { Location = new LocationInfo { Name = "London" } };
    var timezoneResponse = new TimezoneResponse { Location = new LocationInfo { Name = "London" } };
    var astronomyResponse = new AstronomyResponse { Location = new LocationInfo { Name = "London" } };

    _innerClientMock.GetCurrentWeatherAsync("London").Returns(weatherResponse);
    _innerClientMock.GetTimezoneAsync("London").Returns(timezoneResponse);
    _innerClientMock.GetAstronomyAsync("London").Returns(astronomyResponse);

    // When
    var weather = await _cachedClient.GetCurrentWeatherAsync("London");
    var timezone = await _cachedClient.GetTimezoneAsync("London");
    var astronomy = await _cachedClient.GetAstronomyAsync("London");

    // Then
    weather.Should().BeSameAs(weatherResponse);
    timezone.Should().BeSameAs(timezoneResponse);
    astronomy.Should().BeSameAs(astronomyResponse);

    await _innerClientMock.Received(1).GetCurrentWeatherAsync("London");
    await _innerClientMock.Received(1).GetTimezoneAsync("London");
    await _innerClientMock.Received(1).GetAstronomyAsync("London");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetCurrentWeatherCalledWithInvalidCity(string? city)
  {
    // Given
    var act = () => _cachedClient.GetCurrentWeatherAsync(city!);

    // When / Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetTimezoneCalledWithInvalidCity(string? city)
  {
    // Given
    var act = () => _cachedClient.GetTimezoneAsync(city!);

    // When / Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetAstronomyCalledWithInvalidCity(string? city)
  {
    // Given
    var act = () => _cachedClient.GetAstronomyAsync(city!);

    // When / Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }
}

