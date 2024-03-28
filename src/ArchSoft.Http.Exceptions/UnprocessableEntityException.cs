using System.Net;

namespace ArchSoft.Http.Exceptions;

public class UnprocessableEntityException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.UnprocessableEntity;

    public UnprocessableEntityException()
    {
    }

    public UnprocessableEntityException(string message) : base(message)
    {
    }

    public UnprocessableEntityException(string message, Exception inner) : base(message, inner)
    {
    }
}
