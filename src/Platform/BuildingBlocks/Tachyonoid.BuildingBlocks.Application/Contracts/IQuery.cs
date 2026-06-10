using Tachyonoid.BuildingBlocks.Domain;

namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// CQRS query. Read-only. Returns Result&lt;T&gt;.
/// </summary>
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
