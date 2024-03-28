using System.Net;

namespace ArchSoft.Http.Exceptions;

public class RequestTimeoutException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.RequestTimeout;

    public RequestTimeoutException()
    {
    }

    public RequestTimeoutException(string message) : base(message)
    {
    }

    public RequestTimeoutException(string message, Exception inner) : base(message, inner)
    {
    }
}
