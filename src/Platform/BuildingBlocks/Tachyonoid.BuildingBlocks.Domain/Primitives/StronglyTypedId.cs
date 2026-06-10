namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// Base record for strongly-typed IDs. Prevents primitive obsession.
/// </summary>
public abstract record StronglyTypedId<T>(T Value) where T : notnull;

/// <summary>
/// Strongly-typed Guid ID (most common).
/// </summary>
public abstract record StronglyTypedGuidId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static T New<T>() where T : StronglyTypedGuidId
    {
        var ctor = typeof(T).GetConstructor(new[] { typeof(Guid) })
            ?? throw new InvalidOperationException($"Type {typeof(T).Name} needs constructor(Guid)");
        return (T)ctor.Invoke(new object[] { Guid.NewGuid() });
    }
}

/// <summary>
/// Strongly-typed string ID.
/// </summary>
public abstract record StronglyTypedStringId(string Value) : StronglyTypedId<string>(Value);
