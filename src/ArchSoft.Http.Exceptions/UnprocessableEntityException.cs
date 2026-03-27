using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UnprocessableEntityException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.UnprocessableEntity;

        public UnprocessableEntityException()
        {
        }

        public UnprocessableEntityException(string message) : base(message)
        {
        }

        public UnprocessableEntityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
