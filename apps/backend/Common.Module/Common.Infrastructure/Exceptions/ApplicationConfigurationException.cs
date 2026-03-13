namespace Common.Infrastructure.Exceptions;

public class ApplicationConfigurationException : InfrastructureBaseException
{
  public ApplicationConfigurationException(string message) : base(message)
  {
  }

  public ApplicationConfigurationException()
  {
  }

  public ApplicationConfigurationException(string message, Exception innerException) : base(message, innerException)
  {
  }
}
