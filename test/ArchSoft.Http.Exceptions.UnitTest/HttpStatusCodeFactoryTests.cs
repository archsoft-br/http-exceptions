using System.Net;
using ArchSoft.Http.Exceptions.Factories;

namespace ArchSoft.Http.Exceptions.UnitTest
{
    public class HttpStatusCodeFactoryTests
    {
        [Theory]
        [InlineData(typeof(BadGatewayException), HttpStatusCode.BadGateway)]
        [InlineData(typeof(BadRequestException), HttpStatusCode.BadRequest)]
        [InlineData(typeof(ArgumentException), HttpStatusCode.BadRequest)]
        [InlineData(typeof(ConflictException), HttpStatusCode.Conflict)]
        [InlineData(typeof(InvalidOperationException), HttpStatusCode.Conflict)]
        [InlineData(typeof(ForbiddenException), HttpStatusCode.Forbidden)]
        [InlineData(typeof(GatewayTimeoutException), HttpStatusCode.GatewayTimeout)]
        [InlineData(typeof(GoneException), HttpStatusCode.Gone)]
        [InlineData(typeof(InsufficientStorageException), HttpStatusCode.InsufficientStorage)]
        [InlineData(typeof(LoopDetectedException), HttpStatusCode.LoopDetected)]
        [InlineData(typeof(NotFoundException), HttpStatusCode.NotFound)]
        [InlineData(typeof(NotImplementedException), HttpStatusCode.NotImplemented)]
        [InlineData(typeof(RequestTimeoutException), HttpStatusCode.RequestTimeout)]
        [InlineData(typeof(TimeoutException), HttpStatusCode.RequestTimeout)]
        [InlineData(typeof(ServiceUnavailableException), HttpStatusCode.ServiceUnavailable)]
        [InlineData(typeof(UnauthorizedException), HttpStatusCode.Unauthorized)]
        [InlineData(typeof(UnprocessableEntityException), HttpStatusCode.UnprocessableEntity)]
        [InlineData(typeof(MethodNotAllowedException), HttpStatusCode.MethodNotAllowed)]
        [InlineData(typeof(NotAcceptableException), HttpStatusCode.NotAcceptable)]
        [InlineData(typeof(PreconditionFailedException), HttpStatusCode.PreconditionFailed)]
        [InlineData(typeof(PayloadTooLargeException), HttpStatusCode.RequestEntityTooLarge)]
        [InlineData(typeof(TooManyRequestsException), HttpStatusCode.TooManyRequests)]
        [InlineData(typeof(UnsupportedMediaTypeException), HttpStatusCode.UnsupportedMediaType)]
        public void Create_WithMappedException_ReturnsCorrectStatusCode(Type exceptionType, HttpStatusCode expectedStatusCode)
        {
            var exception = (Exception)Activator.CreateInstance(exceptionType, "Test message")!;

            var statusCode = HttpStatusCodeFactory.Create(exception);

            Assert.Equal(expectedStatusCode, statusCode);
        }

        [Fact]
        public void Create_WithUnknownException_ReturnsInternalServerError()
        {
            var exception = new Exception("Unknown exception");

            var statusCode = HttpStatusCodeFactory.Create(exception);

            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void Create_WithNullException_ReturnsInternalServerError()
        {
            Exception? exception = null;

            var statusCode = HttpStatusCodeFactory.Create(exception);

            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
