namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// Marker interface for a request that returns a response.
/// Handlers implement IRequestHandler&lt;TRequest, TResponse&gt;.
/// </summary>
public interface IRequest<out TResponse> { }

/// <summary>
/// Marker interface for a request with no response (fire-and-forget).
/// </summary>
public interface IRequest { }

/// <summary>
/// Handles a request and returns a response.
/// </summary>
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    ValueTask<TResponse> Handle(TRequest request, CancellationToken ct);
}

/// <summary>
/// Handles a request with no response.
/// </summary>
public interface IRequestHandler<in TRequest> where TRequest : IRequest
{
    ValueTask Handle(TRequest request, CancellationToken ct);
}

/// <summary>
/// Pipeline behavior that wraps around request handlers.
/// Enables cross-cutting concerns: logging, validation, transactions, etc.
/// </summary>
public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    ValueTask<TResponse> Handle(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TRequest, TResponse> next);
}

/// <summary>
/// Delegate representing the next handler/behavior in the pipeline.
/// </summary>
public delegate ValueTask<TResponse> RequestHandlerDelegate<TRequest, TResponse>(
    TRequest request,
    CancellationToken ct);

/// <summary>
/// Entry point for dispatching requests through the mediator pipeline.
/// Carter modules depend on this; no direct handler references in API layer.
/// </summary>
public interface ISender
{
    ValueTask<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
    ValueTask Send(IRequest request, CancellationToken ct = default);
}
