using System.Net;

namespace ArchSoft.Http.Exceptions;

public class ForbiddenException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.Forbidden;

    public ForbiddenException()
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception inner) : base(message, inner)
    {
    }
}
