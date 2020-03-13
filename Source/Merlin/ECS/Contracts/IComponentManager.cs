using System;

namespace Merlin.ECS.Contracts
{
    public interface IComponentManager : IComponentOwner
    {
        void AddComponent(Component component);
        void AddComponents(params Component[] components);
        Component RemoveComponentOfType(Type type);
        Component RemoveComponent<T>() where T : Component;
        void ClearComponents();

        Entity WithComponents(params Component[] components);
    }
}
