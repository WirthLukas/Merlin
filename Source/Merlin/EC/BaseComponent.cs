
namespace Merlin.EC;

public abstract class BaseComponent : IInitializable
{
    public Entity Entity { get; protected set; } = null!;

    public virtual void Initialize(Entity entity) => Entity = entity;

    public T? GetComponent<T>() => Entity.GetComponent<T>();
}