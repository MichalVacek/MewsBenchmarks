using FuncSharp;

namespace MewsBenchmarks.OptionalEntity;

public class Entity
{
    public Guid Id { get; set; }

    public Guid? OptionalEntityId { get; set; }

    public Entity OptionalEntityValue { get; set; }

    public Entity? SafeOptionalEntity
    {
        get => ToOptional(OptionalEntityValue, OptionalEntityId);
        internal set
        {
            OptionalEntityValue = value;
            OptionalEntityId = OptionalEntityValue.MapRefToVal(v => v.Id);
        }
    }

    public Option<Entity> OptionalEntity
    {
        get => ToOption(OptionalEntityValue, OptionalEntityId);
        internal set
        {
            OptionalEntityValue = value.GetOrNull();
            OptionalEntityId = OptionalEntityValue.MapRefToVal(v => v.Id);
        }
    }

    protected Option<T> ToOption<T>(T entity, Guid? entityId)
        where T : class
    {
        var isEntityLoaded = entity is not null || entityId is null;
        if (!isEntityLoaded)
        {
            throw new Exception($"Property {typeof(T).Name} on entity {GetType().Name} is not loaded.");
        }
        return entity.ToOption();
    }

    protected T? ToOptional<T>(T entity, Guid? entityId)
        where T : class
    {
        var isEntityLoaded = entity is not null || entityId is null;
        if (!isEntityLoaded)
        {
            throw new Exception($"Property {typeof(T).Name} on entity {GetType().Name} is not loaded.");
        }
        return entity;
    }
}
