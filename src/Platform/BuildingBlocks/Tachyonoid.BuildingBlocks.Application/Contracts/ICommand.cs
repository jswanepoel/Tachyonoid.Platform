using Tachyonoid.BuildingBlocks.Domain;

namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// CQRS command. Mutates state. Returns Result or Result&lt;T&gt;.
/// </summary>
public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
