using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.NotFound;

        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
