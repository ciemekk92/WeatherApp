using System.Net;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.Services.Implementations;
using Common.Infrastructure.Services.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace Common.Test.Unit.Services;

public class WeatherApiClientTests
{
  private const string BaseUrl = "https://api.example.com";
  private const string ApiKey = "test-api-key";

  private static WeatherApiClient CreateClient(HttpMessageHandler handler)
  {
    var httpClient = new HttpClient(handler);
    var secretsProvider = Substitute.For<ISecretsProvider>();

    secretsProvider.GetSecretAsync("WEATHER_API_BASE_URL").Returns(BaseUrl);
    secretsProvider.GetSecretAsync("WEATHER_API_KEY").Returns(ApiKey);

    return new WeatherApiClient(httpClient, secretsProvider);
  }

  private static FakeHttpMessageHandler CreateHandler(HttpStatusCode statusCode, string content = "{}")
    => new(new HttpResponseMessage(statusCode) { Content = new StringContent(content) });

  [Fact]
  public async Task ShouldThrowNotFoundExceptionWhenGetCurrentWeatherReturns404()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.NotFound));

    // When
    var act = () => client.GetCurrentWeatherAsync("NonExistentCity");

    // Then
    await act.Should().ThrowAsync<NotFoundException>()
      .WithMessage("*NonExistentCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetCurrentWeatherReturns400()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.BadRequest));

    // When
    var act = () => client.GetCurrentWeatherAsync("InvalidCity");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*InvalidCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetCurrentWeatherReturns500()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.InternalServerError));

    // When
    var act = () => client.GetCurrentWeatherAsync("London");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*London*");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetCurrentWeatherCalledWithInvalidCity(string? city)
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.OK));

    // When
    var act = () => client.GetCurrentWeatherAsync(city!);

    // Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }

  [Fact]
  public async Task ShouldThrowNotFoundExceptionWhenGetTimezoneReturns404()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.NotFound));

    // When
    var act = () => client.GetTimezoneAsync("NonExistentCity");

    // Then
    await act.Should().ThrowAsync<NotFoundException>()
      .WithMessage("*NonExistentCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetTimezoneReturns400()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.BadRequest));

    // When
    var act = () => client.GetTimezoneAsync("InvalidCity");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*InvalidCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetTimezoneReturns500()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.InternalServerError));

    // When
    var act = () => client.GetTimezoneAsync("London");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*London*");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetTimezoneCalledWithInvalidCity(string? city)
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.OK));

    // When
    var act = () => client.GetTimezoneAsync(city!);

    // Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }

  [Fact]
  public async Task ShouldThrowNotFoundExceptionWhenGetAstronomyReturns404()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.NotFound));

    // When
    var act = () => client.GetAstronomyAsync("NonExistentCity");

    // Then
    await act.Should().ThrowAsync<NotFoundException>()
      .WithMessage("*NonExistentCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetAstronomyReturns400()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.BadRequest));

    // When
    var act = () => client.GetAstronomyAsync("InvalidCity");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*InvalidCity*");
  }

  [Fact]
  public async Task ShouldThrowApplicationLogicExceptionWhenGetAstronomyReturns500()
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.InternalServerError));

    // When
    var act = () => client.GetAstronomyAsync("London");

    // Then
    await act.Should().ThrowAsync<ApplicationLogicException>()
      .WithMessage("*London*");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetAstronomyCalledWithInvalidCity(string? city)
  {
    // Given
    var client = CreateClient(CreateHandler(HttpStatusCode.OK));

    // When
    var act = () => client.GetAstronomyAsync(city!);

    // Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("city");
  }

  private sealed class FakeHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
  {
    protected override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request, CancellationToken cancellationToken)
      => Task.FromResult(response);
  }
}
