namespace Knab.Exchange.Infrastructure.Common.Exceptions;

public class GeneralApplicationException : ApplicationException, IApplicationException
{
    public GeneralApplicationException(string message) : base(message)
    {
    }
}