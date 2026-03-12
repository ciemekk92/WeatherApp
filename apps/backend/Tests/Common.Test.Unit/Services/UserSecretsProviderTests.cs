using Common.Infrastructure.Services.Implementations;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Common.Test.Unit.Services;

public class UserSecretsProviderTests
{
  private readonly IConfiguration _configurationMock;
  private readonly UserSecretsProvider _provider;

  public UserSecretsProviderTests()
  {
    _configurationMock = Substitute.For<IConfiguration>();
    _provider = new UserSecretsProvider(_configurationMock);
  }

  [Fact]
  public async Task ShouldReturnValueWhenKeyExists()
  {
    // Given
    _configurationMock["WEATHER_API_KEY"].Returns("my-secret-key");

    // When
    var result = await _provider.GetSecretAsync("WEATHER_API_KEY");

    // Then
    result.Should().Be("my-secret-key");
  }

  [Fact]
  public async Task ShouldReturnNullWhenKeyDoesNotExist()
  {
    // Given
    _configurationMock["NON_EXISTENT_KEY"].Returns((string?)null);

    // When
    var result = await _provider.GetSecretAsync("NON_EXISTENT_KEY");

    // Then
    result.Should().BeNull();
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetSecretCalledWithInvalidKey(string? key)
  {
    // Given
    var act = () => _provider.GetSecretAsync(key!);

    // When / Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("key");
  }

  [Fact]
  public async Task ShouldReturnValueWhenKeyExistsIgnoringDefault()
  {
    // Given
    _configurationMock["WEATHER_API_KEY"].Returns("real-value");

    // When
    var result = await _provider.GetSecretAsync("WEATHER_API_KEY", "fallback-value");

    // Then
    result.Should().Be("real-value");
  }

  [Fact]
  public async Task ShouldReturnDefaultValueWhenKeyDoesNotExist()
  {
    // Given
    _configurationMock["NON_EXISTENT_KEY"].Returns((string?)null);

    // When
    var result = await _provider.GetSecretAsync("NON_EXISTENT_KEY", "fallback-value");

    // Then
    result.Should().Be("fallback-value");
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public async Task ShouldThrowArgumentExceptionWhenGetSecretWithDefaultCalledWithInvalidKey(string? key)
  {
    // Given
    var act = () => _provider.GetSecretAsync(key!, "some-default");

    // When / Then
    await act.Should().ThrowAsync<ArgumentException>().WithParameterName("key");
  }
}
