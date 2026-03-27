# ArchSoft.Http.Exceptions

[![NuGet](https://img.shields.io/nuget/v/ArchSoft.Http.Exceptions.svg)](https://www.nuget.org/packages/ArchSoft.Http.Exceptions/)
[![License](https://img.shields.io/github/license/archsoft-br/http-exceptions.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue.svg)](https://dotnet.microsoft.com/)

Uma biblioteca .NET que fornece **exceções HTTP fortemente tipadas** para construção de APIs REST robustas. Inclui classes factory para conversão bidirecional entre códigos de status HTTP e exceções.

[📄 Documentation in English](README.md)

## 🚀 Instalação

```bash
dotnet add package ArchSoft.Http.Exceptions
```

## ✨ Recursos

- 🎯 **Mais de 20 Exceções Tipadas** - Classes de exceções específicas para cada código de status HTTP
- 🔄 **HttpExceptionFactory** - Converte `HttpStatusCode` para exceções tipadas
- 🔙 **HttpStatusCodeFactory** - Converte exceções para `HttpStatusCode` com mapeamento automático
- 🧩 **Suporte a Exceções Nativas** - Mapeamento automático de `ArgumentException`, `InvalidOperationException`, etc.
- ✅ **Totalmente Testada** - Suíte completa de testes unitários com xUnit
- ⚡ **Zero Dependências** - Biblioteca leve sem dependências externas

## 📖 Casos de Uso

- **APIs REST** - Lançar exceções tipadas a partir de controllers e serviços
- **Middleware de Exceções** - Tratamento centralizado de erros no ASP.NET Core
- **Clientes de API** - Converter respostas de erro HTTP em exceções tipadas
- **Microserviços** - Tratamento de erros padronizado entre serviços
- **Serviços de Domínio** - Validação de regras de negócio com status HTTP apropriado

## 📋 Exceções Disponíveis

| Exceção | Código | Quando Usar |
|---------|--------|--------------|
| `BadRequestException` | 400 | Dados de requisição inválidos ou sintaxe malformada |
| `UnauthorizedException` | 401 | Autenticação ausente ou inválida |
| `ForbiddenException` | 403 | Autenticado, mas não autorizado |
| `NotFoundException` | 404 | O recurso não existe |
| `MethodNotAllowedException` | 405 | O método HTTP não é suportado pelo recurso |
| `NotAcceptableException` | 406 | O recurso não consegue gerar conteúdo aceitável segundo o cabeçalho Accept |
| `RequestTimeoutException` | 408 | A requisição demorou muito para ser processada |
| `ConflictException` | 409 | Conflito de estado do recurso |
| `GoneException` | 410 | O recurso foi removido permanentemente |
| `PreconditionFailedException` | 412 | Uma ou mais condições nos cabeçalhos da requisição falharam |
| `PayloadTooLargeException` | 413 | A entidade da requisição é maior que os limites definidos pelo servidor |
| `UnsupportedMediaTypeException` | 415 | O formato de mídia dos dados solicitados não é suportado |
| `UnprocessableEntityException` | 422 | Sintaxe válida, mas com erros semânticos |
| `TooManyRequestsException` | 429 | O usuário enviou muitas solicitações em um determinado período |
| `InternalServerErrorException` | 500 | Erro inesperado do servidor |
| `NotImplementedException` | 501 | Funcionalidade ainda não implementada |
| `BadGatewayException` | 502 | Resposta inválida de um servidor upstream |
| `ServiceUnavailableException` | 503 | Serviço temporariamente indisponível |
| `GatewayTimeoutException` | 504 | Tempo limite excedido do servidor upstream |
| `InsufficientStorageException` | 507 | O servidor não consegue armazenar a representação |
| `LoopDetectedException` | 508 | Loop infinito detectado |

## 💻 Uso Básico

### Lançando Exceções em Serviços

```csharp
using ArchSoft.Http.Exceptions;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Usuario> ObterPorIdAsync(int id)
    {
        var usuario = await _repository.FindByIdAsync(id);

        if (usuario == null)
            throw new NotFoundException($"Usuário com ID {id} não foi encontrado");

        return usuario;
    }
}
```

### Erros de Validação

```csharp
public class CriarUsuarioService
{
    public async Task<Usuario> CriarAsync(CriarUsuarioRequest request)
    {
        // Dados inválidos - 400 Bad Request
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BadRequestException("Email é obrigatório");

        if (!EmailValido(request.Email))
            throw new BadRequestException("Formato de email inválido");

        // Conflito de negócio - 409 Conflict
        if (await _repository.EmailExisteAsync(request.Email))
            throw new ConflictException($"Email '{request.Email}' já está cadastrado");

        return await _repository.CriarAsync(request);
    }
}
```

### Autenticação & Autorização

```csharp
public class PedidoService
{
    public async Task<Pedido> ObterPedidoAsync(int pedidoId, int usuarioId)
    {
        // Não autenticado - 401 Unauthorized
        if (usuarioId == 0)
            throw new UnauthorizedException("Autenticação necessária para acessar pedidos");

        var pedido = await _repository.FindByIdAsync(pedidoId);

        // Não encontrado - 404 Not Found
        if (pedido == null)
            throw new NotFoundException($"Pedido {pedidoId} não foi encontrado");

        // Acesso negado - 403 Forbidden
        if (pedido.UsuarioId != usuarioId && !await _usuarioService.EhAdminAsync(usuarioId))
            throw new ForbiddenException("Você não tem permissão para acessar este pedido");

        return pedido;
    }
}
```

## 🔧 Exemplos Avançados

### Padrão Soft Delete

```csharp
public class DocumentoService
{
    public async Task<Documento> ObterDocumentoAsync(int id)
    {
        var documento = await _repository.FindByIdAsync(id);

        if (documento == null)
            throw new NotFoundException($"Documento {id} não encontrado");

        // Documento foi removido via soft delete - 410 Gone
        if (documento.Excluido)
            throw new GoneException($"Documento {id} foi removido permanentemente");

        return documento;
    }
}
```

### Validação de Regra de Negócio

```csharp
public class PagamentoService
{
    public async Task ProcessarPagamentoAsync(PagamentoRequest request)
    {
        // Inválido semanticamente - 422 Unprocessable Entity
        if (request.Valor <= 0)
            throw new UnprocessableEntityException("Valor do pagamento deve ser maior que zero");

        if (request.Valor > _valorMaximoPagamento)
            throw new UnprocessableEntityException($"Valor excede o limite máximo de {_valorMaximoPagamento}");

        // Processar pagamento...
    }
}
```

### Integração com Serviço Externo

```csharp
public class ApiExternaService
{
    public async Task<ApiResponse> ChamarServicoExternoAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/externo");

            if (!response.IsSuccessStatusCode)
            {
                var mensagem = await response.Content.ReadAsStringAsync();
                throw HttpExceptionFactory.Create(response.StatusCode, mensagem);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }
        catch (HttpRequestException ex)
        {
            // Erro de serviço upstream - 502 Bad Gateway
            throw new BadGatewayException("Serviço externo retornou uma resposta inválida", ex);
        }
    }
}
```

### Modo de Manutenção

```csharp
public class ManutencaoMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (_manutencaoService.EmManutencao())
        {
            // Serviço temporariamente indisponível - 503 Service Unavailable
            throw new ServiceUnavailableException(
                "Serviço em manutenção. Por favor, tente novamente mais tarde.");
        }

        await _next(context);
    }
}
```

## 🔄 Usando Factories

### HttpExceptionFactory - Converter StatusCode para Exceção

```csharp
using ArchSoft.Http.Exceptions.Factories;
using System.Net;

public class ApiClient
{
    public async Task<T> ObterAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw HttpExceptionFactory.Create(response.StatusCode, erro);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
```

### HttpStatusCodeFactory - Converter Exceção para StatusCode

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

### Mapeamento de Exceções Nativas

```csharp
// Exceções nativas do .NET são mapeadas automaticamente:
// ArgumentException          → 400 Bad Request
// InvalidOperationException  → 409 Conflict
// TimeoutException           → 408 Request Timeout
// NotImplementedException    → 501 Not Implemented

public async Task<IActionResult> ProcessarPedido(int pedidoId)
{
    try
    {
        await _pedidoService.ProcessarAsync(pedidoId);
        return Ok();
    }
    catch (ArgumentException ex)
    {
        // HttpStatusCodeFactory retorna 400 Bad Request
        var statusCode = HttpStatusCodeFactory.Create(ex);
        return StatusCode((int)statusCode, ex.Message);
    }
}
```

## 📝 Exemplo Completo de Tratamento de Exceções

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

        _logger.LogError(exception, "Requisição falhou com status {StatusCode}", statusCode);

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

## ⚠️ Notas Importantes

- Todas as exceções herdam de `System.Exception` com três construtores
- `HttpStatusCodeFactory.Create(null)` retorna `500 Internal Server Error`
- Exceções desconhecidas retornam `500 Internal Server Error`
- Use `HttpExceptionFactory` para converter códigos de status de APIs externas

## 📋 Requisitos

- .NET 8.0 ou superior

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor, abra uma issue ou pull request.

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 🏢 Sobre

Desenvolvido por [ArchSoft](https://github.com/archsoft-br) - Soluções de software.