using System.Net;

namespace ArchSoft.Http.Exceptions;

public class GatewayTimeoutException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.GatewayTimeout;

    public GatewayTimeoutException()
    {
    }

    public GatewayTimeoutException(string message) : base(message)
    {
    }

    public GatewayTimeoutException(string message, Exception inner) : base(message, inner)
    {
    }
}
