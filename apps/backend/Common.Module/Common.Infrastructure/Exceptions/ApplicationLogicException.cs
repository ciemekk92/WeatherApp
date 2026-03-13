namespace Common.Infrastructure.Exceptions;

public class ApplicationLogicException : Exception
{
  public ApplicationLogicException(string message, Exception innerException) : base(message, innerException)
  {
  }

  public ApplicationLogicException()
  {
  }

  public ApplicationLogicException(string message) : base(message)
  {
  }
}
