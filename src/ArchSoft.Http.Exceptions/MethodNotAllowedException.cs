using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class MethodNotAllowedException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.MethodNotAllowed;

        public MethodNotAllowedException()
        {
        }

        public MethodNotAllowedException(string message) : base(message)
        {
        }

        public MethodNotAllowedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
