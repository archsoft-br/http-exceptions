using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotAcceptableException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.NotAcceptable;

        public NotAcceptableException()
        {
        }

        public NotAcceptableException(string message) : base(message)
        {
        }

        public NotAcceptableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
