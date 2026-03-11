namespace Common.Infrastructure.Exceptions;

[Serializable]
public class InfrastructureBaseException : Exception
{
  protected InfrastructureBaseException(string message, Exception innerException) : base(message, innerException)
  {
  }

  protected InfrastructureBaseException()
  {
  }

  protected InfrastructureBaseException(string message) : base(message)
  {
  }
}
