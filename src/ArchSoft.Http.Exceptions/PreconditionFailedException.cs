using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class PreconditionFailedException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.PreconditionFailed;

        public PreconditionFailedException()
        {
        }

        public PreconditionFailedException(string message) : base(message)
        {
        }

        public PreconditionFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
