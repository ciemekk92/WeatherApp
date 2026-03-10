using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Common.Infrastructure.Services.Interfaces;

namespace Common.Infrastructure.Services.Implementations;
public static class SecretsProviderFactory
{
  public static ISecretsProvider CreateSecretsProvider(
    IConfiguration configuration,
    IHostEnvironment environment)
  {
    if (environment.IsDevelopment())
    {
      return new UserSecretsProvider(configuration);
    }

    // Fallback for additional providers per environment (e.g., production)
    return new UserSecretsProvider(configuration);
  }
}
