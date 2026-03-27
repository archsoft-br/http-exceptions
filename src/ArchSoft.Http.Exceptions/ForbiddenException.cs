using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
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
}
