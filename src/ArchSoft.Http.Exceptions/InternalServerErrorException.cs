using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InternalServerErrorException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;

        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
