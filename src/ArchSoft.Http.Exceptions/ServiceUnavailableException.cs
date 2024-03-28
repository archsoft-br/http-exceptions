using System.Net;

namespace ArchSoft.Http.Exceptions;

public class ServiceUnavailableException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.ServiceUnavailable;

    public ServiceUnavailableException()
    {
    }

    public ServiceUnavailableException(string message) : base(message)
    {
    }

    public ServiceUnavailableException(string message, Exception inner) : base(message, inner)
    {
    }
}
