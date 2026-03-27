using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ConflictException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.Conflict;

        public ConflictException()
        {
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
