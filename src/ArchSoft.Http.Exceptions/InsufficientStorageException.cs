using System.Net;

namespace ArchSoft.Http.Exceptions;

public class InsufficientStorageException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.InsufficientStorage;

    public InsufficientStorageException()
    {
    }

    public InsufficientStorageException(string message) : base(message)
    {
    }

    public InsufficientStorageException(string message, Exception inner) : base(message, inner)
    {
    }
}
