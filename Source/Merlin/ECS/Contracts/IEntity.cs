using System;

namespace Merlin.ECS.Contracts
{
    public interface IEntity : IComparable<IEntity>, IComparable
    {
        ulong Id { get; }
        string Name { get; }
        bool Destroyed { get; }

        TComponent GetComponent<TComponent>(bool inherited = false) where TComponent : class, IComponent;
        bool HasComponent<TComponent>() where TComponent : class, IComponent;
        bool HasComponentOfType(Type componentType);
        void Destroy();

        event DestroyedHandler DestroyedChanged;
    }
}