using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class PayloadTooLargeException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.RequestEntityTooLarge;

        public PayloadTooLargeException()
        {
        }

        public PayloadTooLargeException(string message) : base(message)
        {
        }

        public PayloadTooLargeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
