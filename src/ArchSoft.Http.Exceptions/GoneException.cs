using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class GoneException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.Gone;

        public GoneException()
        {
        }

        public GoneException(string message) : base(message)
        {
        }

        public GoneException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
