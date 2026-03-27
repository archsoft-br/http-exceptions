using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BadRequestException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
