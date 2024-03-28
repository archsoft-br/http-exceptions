# Library with a compilation of HTTP Exceptions

- BadGatewayException
- BadRequestException
- ConflictException
- ForbiddenException
- GatewayTimeoutException
- GoneException
- InsufficientStorageException
- LoopDetectedException
- NotFoundException
- RequestTimeoutException
- ServiceUnavailableException
- UnauthorizedException
- UnprocessableEntityException
- InternalServerErrorException

## Factories

Use factory classes to simplify exception handling and status code returns in REST APIs:

```csharp
var ex = HttpExceptionFactory.Create(statusCode, message);
```

```csharp
var statusCode = HttpStatusCodeFactory.Create(ex);
```
