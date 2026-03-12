namespace Common.Infrastructure.Exceptions;

[Serializable]
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
