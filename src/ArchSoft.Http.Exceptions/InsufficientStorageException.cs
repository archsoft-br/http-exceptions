using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InsufficientStorageException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.InsufficientStorage;

        public InsufficientStorageException()
        {
        }

        public InsufficientStorageException(string message) : base(message)
        {
        }

        public InsufficientStorageException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
