using System.Net;

namespace ArchSoft.Http.Exceptions.Factories;

public static class HttpExceptionFactory
{
    public static Exception Create(HttpStatusCode statusCode, string message)
    {
        switch (statusCode)
        {
            case HttpStatusCode.BadGateway:
                return new BadGatewayException(message);

            case HttpStatusCode.BadRequest:
                return new BadRequestException(message);

            case HttpStatusCode.Conflict:
                return new ConflictException(message);
            
            case HttpStatusCode.Forbidden:
                return new ForbiddenException(message);
            
            case HttpStatusCode.GatewayTimeout:
                return new GatewayTimeoutException(message);
            
            case HttpStatusCode.Gone:
                return new GoneException(message);
            
            case HttpStatusCode.InsufficientStorage:
                return new InsufficientStorageException(message);
            
            case HttpStatusCode.LoopDetected:
                return new LoopDetectedException(message);
            
            case HttpStatusCode.NotFound:
                return new NotFoundException(message);
            
            case HttpStatusCode.NotImplemented:
                return new NotImplementedException(message);
            
            case HttpStatusCode.RequestTimeout:
                return new RequestTimeoutException(message);
            
            case HttpStatusCode.ServiceUnavailable:
                return new ServiceUnavailableException(message);
            
            case HttpStatusCode.Unauthorized:
                return new UnauthorizedException(message);
            
            case HttpStatusCode.UnprocessableEntity:
                return new UnprocessableEntityException(message);
            
            default:
                return new InternalServerErrorException(message);
        }
    }
}
