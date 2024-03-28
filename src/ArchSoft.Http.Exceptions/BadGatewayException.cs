using System.Net;

namespace ArchSoft.Http.Exceptions;

public class BadGatewayException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.BadGateway;

    public BadGatewayException()
    {
    }

    public BadGatewayException(string message) : base(message)
    {
    }

    public BadGatewayException(string message, Exception inner) : base(message, inner)
    {
    }
}
