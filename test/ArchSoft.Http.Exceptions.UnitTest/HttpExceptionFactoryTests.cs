using System.Net;
using ArchSoft.Http.Exceptions.Factories;

namespace ArchSoft.Http.Exceptions.UnitTest
{
    public class HttpExceptionFactoryTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadGateway, typeof(BadGatewayException))]
        [InlineData(HttpStatusCode.BadRequest, typeof(BadRequestException))]
        [InlineData(HttpStatusCode.Conflict, typeof(ConflictException))]
        [InlineData(HttpStatusCode.Forbidden, typeof(ForbiddenException))]
        [InlineData(HttpStatusCode.GatewayTimeout, typeof(GatewayTimeoutException))]
        [InlineData(HttpStatusCode.Gone, typeof(GoneException))]
        [InlineData(HttpStatusCode.InsufficientStorage, typeof(InsufficientStorageException))]
        [InlineData(HttpStatusCode.LoopDetected, typeof(LoopDetectedException))]
        [InlineData(HttpStatusCode.NotFound, typeof(NotFoundException))]
        [InlineData(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        [InlineData(HttpStatusCode.RequestTimeout, typeof(RequestTimeoutException))]
        [InlineData(HttpStatusCode.ServiceUnavailable, typeof(ServiceUnavailableException))]
        [InlineData(HttpStatusCode.Unauthorized, typeof(UnauthorizedException))]
        [InlineData(HttpStatusCode.UnprocessableEntity, typeof(UnprocessableEntityException))]
        [InlineData(HttpStatusCode.MethodNotAllowed, typeof(MethodNotAllowedException))]
        [InlineData(HttpStatusCode.NotAcceptable, typeof(NotAcceptableException))]
        [InlineData(HttpStatusCode.PreconditionFailed, typeof(PreconditionFailedException))]
        [InlineData(HttpStatusCode.RequestEntityTooLarge, typeof(PayloadTooLargeException))]
        [InlineData(HttpStatusCode.TooManyRequests, typeof(TooManyRequestsException))]
        [InlineData(HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeException))]
        public void Create_WithValidStatusCode_ReturnsCorrectExceptionType(HttpStatusCode statusCode, Type expectedType)
        {
            var message = "Test error message";

            var exception = HttpExceptionFactory.Create(statusCode, message);

            Assert.IsType(expectedType, exception);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Create_WithUnknownStatusCode_ReturnsInternalServerErrorException()
        {
            var statusCode = (HttpStatusCode)999;
            var message = "Unknown error";

            var exception = HttpExceptionFactory.Create(statusCode, message);

            Assert.IsType<InternalServerErrorException>(exception);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Create_WithInternalServerErrorStatusCode_ReturnsInternalServerErrorException()
        {
            var message = "Internal server error";

            var exception = HttpExceptionFactory.Create(HttpStatusCode.InternalServerError, message);

            Assert.IsType<InternalServerErrorException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
