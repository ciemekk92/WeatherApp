namespace Common.Infrastructure.Exceptions;

[Serializable]
public class NotFoundException : InfrastructureBaseException
{
  public NotFoundException(string message, Exception innerException) : base(message, innerException)
  {
  }

  public NotFoundException()
  {
  }

  public NotFoundException(string message) : base(message)
  {
  }
}
