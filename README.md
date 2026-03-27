# ArchSoft.Http.Exceptions

[![NuGet](https://img.shields.io/nuget/v/ArchSoft.Http.Exceptions.svg)](https://www.nuget.org/packages/ArchSoft.Http.Exceptions/)
[![License](https://img.shields.io/github/license/archsoft-br/http-exceptions.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue.svg)](https://dotnet.microsoft.com/)

A .NET library that provides **strongly-typed HTTP exceptions** for building robust REST APIs. Includes factory classes for bidirectional conversion between HTTP status codes and exceptions.

[📄 Documentação em Português (Brazilian Portuguese)](README.pt-BR.md)

## 🚀 Installation

```bash
dotnet add package ArchSoft.Http.Exceptions
```

## ✨ Features

- 🎯 **20+ Typed Exceptions** - Specific exception classes for each HTTP status code
- 🔄 **HttpExceptionFactory** - Convert `HttpStatusCode` to typed exceptions
- 🔙 **HttpStatusCodeFactory** - Convert exceptions to `HttpStatusCode` with automatic mapping
- 🧩 **Native Exception Support** - Automatic mapping of `ArgumentException`, `InvalidOperationException`, etc.
- ✅ **Fully Tested** - Complete unit test suite with xUnit
- ⚡ **Zero Dependencies** - Lightweight library with no external dependencies

## 📖 Use Cases

- **REST APIs** - Throw typed exceptions from controllers and services
- **Exception Middleware** - Centralized error handling in ASP.NET Core
- **API Clients** - Convert HTTP error responses to typed exceptions
- **Microservices** - Standardized error handling across services
- **Domain Services** - Business rule validation with appropriate HTTP status

## 📋 Available Exceptions

| Exception | Code | When to Use |
|-----------|------|-------------|
| `BadRequestException` | 400 | Invalid request data or malformed syntax |
| `UnauthorizedException` | 401 | Missing or invalid authentication |
| `ForbiddenException` | 403 | Authenticated but not authorized |
| `NotFoundException` | 404 | Resource does not exist |
| `MethodNotAllowedException` | 405 | HTTP method is not supported for the resource |
| `NotAcceptableException` | 406 | Resource cannot generate content acceptable according to Accept headers |
| `RequestTimeoutException` | 408 | Request took too long to process |
| `ConflictException` | 409 | Resource state conflict |
| `GoneException` | 410 | Resource permanently removed |
| `PreconditionFailedException` | 412 | One or more conditions in the request headers failed |
| `PayloadTooLargeException` | 413 | Request entity is larger than limits defined by server |
| `UnsupportedMediaTypeException` | 415 | Media format of the requested data is not supported |
| `UnprocessableEntityException` | 422 | Valid syntax but semantic errors |
| `TooManyRequestsException` | 429 | User has sent too many requests in a given amount of time |
| `InternalServerErrorException` | 500 | Unexpected server error |
| `NotImplementedException` | 501 | Feature not yet implemented |
| `BadGatewayException` | 502 | Invalid response from upstream |
| `ServiceUnavailableException` | 503 | Service temporarily unavailable |
| `GatewayTimeoutException` | 504 | Upstream server timeout |
| `InsufficientStorageException` | 507 | Server cannot store representation |
| `LoopDetectedException` | 508 | Infinite loop detected |

## 💻 Basic Usage

### Throwing Exceptions in Services

```csharp
using ArchSoft.Http.Exceptions;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _repository.FindByIdAsync(id);

        if (user == null)
            throw new NotFoundException($"User with ID {id} was not found");

        return user;
    }
}
```

### Validation Errors

```csharp
public class CreateUserService
{
    public async Task<User> CreateAsync(CreateUserRequest request)
    {
        // Invalid data - 400 Bad Request
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BadRequestException("Email is required");

        if (!IsValidEmail(request.Email))
            throw new BadRequestException("Email format is invalid");

        // Business conflict - 409 Conflict
        if (await _repository.EmailExistsAsync(request.Email))
            throw new ConflictException($"Email '{request.Email}' is already registered");

        return await _repository.CreateAsync(request);
    }
}
```

### Authentication & Authorization

```csharp
public class OrderService
{
    public async Task<Order> GetOrderAsync(int orderId, int userId)
    {
        // Not authenticated - 401 Unauthorized
        if (userId == 0)
            throw new UnauthorizedException("Authentication required to access orders");

        var order = await _repository.FindByIdAsync(orderId);

        // Not found - 404 Not Found
        if (order == null)
            throw new NotFoundException($"Order {orderId} was not found");

        // Access denied - 403 Forbidden
        if (order.UserId != userId && !await _userService.IsAdminAsync(userId))
            throw new ForbiddenException("You do not have permission to access this order");

        return order;
    }
}
```

## 🔧 Advanced Examples

### Soft Delete Pattern

```csharp
public class DocumentService
{
    public async Task<Document> GetDocumentAsync(int id)
    {
        var document = await _repository.FindByIdAsync(id);

        if (document == null)
            throw new NotFoundException($"Document {id} not found");

        // Document was soft-deleted - 410 Gone
        if (document.IsDeleted)
            throw new GoneException($"Document {id} has been permanently removed");

        return document;
    }
}
```

### Business Rule Validation

```csharp
public class PaymentService
{
    public async Task ProcessPaymentAsync(PaymentRequest request)
    {
        // Semantically invalid - 422 Unprocessable Entity
        if (request.Amount <= 0)
            throw new UnprocessableEntityException("Payment amount must be greater than zero");

        if (request.Amount > _maxPaymentAmount)
            throw new UnprocessableEntityException($"Amount exceeds maximum limit of {_maxPaymentAmount}");

        // Process payment...
    }
}
```

### External Service Integration

```csharp
public class ExternalApiService
{
    public async Task<ApiResponse> CallExternalServiceAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/external");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw HttpExceptionFactory.Create(response.StatusCode, message);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }
        catch (HttpRequestException ex)
        {
            // Upstream service error - 502 Bad Gateway
            throw new BadGatewayException("External service returned an invalid response", ex);
        }
    }
}
```

### Maintenance Mode

```csharp
public class MaintenanceMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (_maintenanceService.IsUnderMaintenance())
        {
            // Service temporarily unavailable - 503 Service Unavailable
            throw new ServiceUnavailableException(
                "Service is under maintenance. Please try again later.");
        }

        await _next(context);
    }
}
```

## 🔄 Using Factories

### HttpExceptionFactory - Convert StatusCode to Exception

```csharp
using ArchSoft.Http.Exceptions.Factories;
using System.Net;

public class ApiClient
{
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw HttpExceptionFactory.Create(response.StatusCode, error);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
```

### HttpStatusCodeFactory - Convert Exception to StatusCode

```csharp
using ArchSoft.Http.Exceptions.Factories;
using System.Net;

public class ExceptionMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = HttpStatusCodeFactory.Create(ex);

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = (int)statusCode,
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
```

### Native Exception Mapping

```csharp
// Native .NET exceptions are automatically mapped:
// ArgumentException          → 400 Bad Request
// InvalidOperationException  → 409 Conflict
// TimeoutException           → 408 Request Timeout
// NotImplementedException    → 501 Not Implemented

public async Task<IActionResult> ProcessOrder(int orderId)
{
    try
    {
        await _orderService.ProcessAsync(orderId);
        return Ok();
    }
    catch (ArgumentException ex)
    {
        // HttpStatusCodeFactory returns 400 Bad Request
        var statusCode = HttpStatusCodeFactory.Create(ex);
        return StatusCode((int)statusCode, ex.Message);
    }
}
```

## 📝 Complete Exception Handling Example

```csharp
// Program.cs - ASP.NET Core
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.Run();

// ExceptionHandlingMiddleware.cs
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCodeFactory.Create(exception);

        _logger.LogError(exception, "Request failed with status {StatusCode}", statusCode);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse(
            Status: (int)statusCode,
            Error: exception.Message,
            Path: context.Request.Path
        );

        await context.Response.WriteAsJsonAsync(response);
    }
}

public record ErrorResponse(int Status, string Error, string Path);
```

## ⚠️ Important Notes

- All exceptions inherit from `System.Exception` with three constructors
- `HttpStatusCodeFactory.Create(null)` returns `500 Internal Server Error`
- Unknown exceptions default to `500 Internal Server Error`
- Use `HttpExceptionFactory` for converting status codes from external APIs

## 📋 Requirements

- .NET 8.0 or higher

## 🤝 Contributing

Contributions are welcome! Please open an issue or pull request.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🏢 About

Developed by [ArchSoft](https://github.com/archsoft-br) - Software solutions.