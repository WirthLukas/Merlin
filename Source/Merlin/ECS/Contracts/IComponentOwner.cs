using System;

namespace Merlin.ECS.Contracts
{
    public interface IComponentOwner
    {
        Component[] Components { get; }
        Component[] ActiveComponents { get; }

        T GetComponent<T>() where T : Component;
        bool HasComponent<T>() where T : Component;
        bool HasComponentOfType(Type type);
    }
}
