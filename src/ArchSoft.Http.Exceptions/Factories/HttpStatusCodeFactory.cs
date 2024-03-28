using System.Net;

namespace ArchSoft.Http.Exceptions.Factories
{
    public static class HttpStatusCodeFactory
    {
        private static readonly Dictionary<Type, HttpStatusCode> Map = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(BadGatewayException), HttpStatusCode.BadGateway },
            { typeof(BadRequestException), HttpStatusCode.BadRequest },
            { typeof(ArgumentException), HttpStatusCode.BadRequest },
            { typeof(ConflictException), HttpStatusCode.Conflict },
            { typeof(InvalidOperationException), HttpStatusCode.Conflict },
            { typeof(ForbiddenException), HttpStatusCode.Forbidden },
            { typeof(GatewayTimeoutException), HttpStatusCode.GatewayTimeout },
            { typeof(GoneException), HttpStatusCode.Gone },
            { typeof(InsufficientStorageException), HttpStatusCode.InsufficientStorage },
            { typeof(LoopDetectedException), HttpStatusCode.LoopDetected },
            { typeof(NotFoundException), HttpStatusCode.NotFound },
            { typeof(NotImplementedException), HttpStatusCode.NotImplemented },
            { typeof(RequestTimeoutException), HttpStatusCode.RequestTimeout },
            { typeof(TimeoutException), HttpStatusCode.RequestTimeout },
            { typeof(ServiceUnavailableException), HttpStatusCode.ServiceUnavailable },
            { typeof(UnauthorizedException), HttpStatusCode.Unauthorized },
            { typeof(UnprocessableEntityException), HttpStatusCode.UnprocessableEntity }            
        };

        public static HttpStatusCode Create(Exception ex)
        {
            return Map.TryGetValue(ex.GetType(), out var statusCode)
                ? statusCode
                : HttpStatusCode.InternalServerError;
        }
    }
}
