namespace Common.Infrastructure.Services.Interfaces;
/// <summary>
/// Abstraction for retrieving secrets from various providers (User Secrets, AWS Secrets Manager, etc.)
/// </summary>
public interface ISecretsProvider
{
  Task<string?> GetSecretAsync(string key);
  Task<string> GetSecretAsync(string key, string defaultValue);
}