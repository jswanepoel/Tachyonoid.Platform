namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// Outbox message entity. Stores integration events atomically with business data.
/// Processed by a background worker.
/// </summary>
public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
}
