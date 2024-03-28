using System.Net;

namespace ArchSoft.Http.Exceptions;

public class GoneException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.Gone;

    public GoneException()
    {
    }

    public GoneException(string message) : base(message)
    {
    }

    public GoneException(string message, Exception inner) : base(message, inner)
    {
    }
}
