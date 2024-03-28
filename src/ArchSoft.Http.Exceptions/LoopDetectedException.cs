using System.Net;

namespace ArchSoft.Http.Exceptions;

public class LoopDetectedException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.LoopDetected;

    public LoopDetectedException()
    {
    }

    public LoopDetectedException(string message) : base(message)
    {
    }

    public LoopDetectedException(string message, Exception inner) : base(message, inner)
    {
    }
}
