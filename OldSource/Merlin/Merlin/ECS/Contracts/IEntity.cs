using System;

namespace Merlin.ECS.Contracts
{
    public interface IEntity : IComparable<IEntity>, IComparable
    {
        ulong Id { get; }
        string Name { get; }
        bool Destroyed { get; }

        T? GetComponent<T>(bool inherited = false) where T : Component;
        bool HasComponent<T>() where T : Component;
        bool HasComponentOfType(Type componentType);
        void Destroy();

        event EventHandler<IEntity>? DestroyedChanged;
    }
}
