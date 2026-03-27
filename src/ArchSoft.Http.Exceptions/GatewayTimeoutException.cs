using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ArchSoft.Http.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class GatewayTimeoutException : Exception
    {
        public static HttpStatusCode StatusCode = HttpStatusCode.GatewayTimeout;

        public GatewayTimeoutException()
        {
        }

        public GatewayTimeoutException(string message) : base(message)
        {
        }

        public GatewayTimeoutException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
