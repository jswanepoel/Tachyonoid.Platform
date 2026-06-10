namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// Pipeline behavior that wraps command handling in a database transaction.
/// Ensures all domain events are dispatched only after successful commit.
/// </summary>
public sealed class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async ValueTask<TResponse> Handle(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TRequest, TResponse> next)
    {
        // In production: begin transaction via IUnitOfWork,
        // call next(), then commit and dispatch domain events.
        // For now, pass through.
        return await next(request, ct);
    }
}
