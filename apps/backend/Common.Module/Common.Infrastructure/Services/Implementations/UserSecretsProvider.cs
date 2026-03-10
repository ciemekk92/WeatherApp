using Microsoft.Extensions.Configuration;
using Common.Infrastructure.Services.Interfaces;

namespace Common.Infrastructure.Services.Implementations;

public class UserSecretsProvider(IConfiguration configuration) : ISecretsProvider
{
  private readonly IConfiguration _configuration = configuration
                                                   ?? throw new ArgumentNullException(nameof(configuration));
  public Task<string?> GetSecretAsync(string key)
  {
    if (string.IsNullOrWhiteSpace(key))
    {
      throw new ArgumentException("Secret key cannot be null or empty.", nameof(key));
    }

    var value = _configuration[key];

    return Task.FromResult(value);
  }

  public Task<string> GetSecretAsync(string key, string defaultValue)
  {
    if (string.IsNullOrWhiteSpace(key))
    {
      throw new ArgumentException("Secret key cannot be null or empty.", nameof(key));
    }

    var value = _configuration[key] ?? defaultValue;

    return Task.FromResult(value);
  }
}
