using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UnsupportedMediaTypeException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.UnsupportedMediaType;

        public UnsupportedMediaTypeException()
        {
        }

        public UnsupportedMediaTypeException(string message) : base(message)
        {
        }

        public UnsupportedMediaTypeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
