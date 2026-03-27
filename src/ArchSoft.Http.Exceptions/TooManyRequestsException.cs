using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class TooManyRequestsException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.TooManyRequests;

        public TooManyRequestsException()
        {
        }

        public TooManyRequestsException(string message) : base(message)
        {
        }

        public TooManyRequestsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
