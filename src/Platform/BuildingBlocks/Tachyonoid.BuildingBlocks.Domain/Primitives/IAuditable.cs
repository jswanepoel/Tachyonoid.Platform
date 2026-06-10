namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// Interface for entities that track creation and modification timestamps.
/// Implemented by domain entities; enforced by EF Core interceptors in persistence layer.
/// </summary>
public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
