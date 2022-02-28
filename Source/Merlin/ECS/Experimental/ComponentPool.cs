using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Merlin.ECS.Experimental;

public class ComponentPool
{
    protected readonly Dictionary<uint, int> _entityIdIndexMap = new();
    protected IComponent[] _components = Array.Empty<IComponent>();

    public IEnumerable Components => Array.AsReadOnly(_components);

    public void Add(IComponent component, uint entityId)
    {
        int oldSize = _components.Length;
        Array.Resize(ref _components, oldSize + 1);
        _components[oldSize] = component;       // oldSize is exactly _components.Length (the new size) - 1, which is the last entry of the array
        _entityIdIndexMap.Add(entityId, oldSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref IComponent GetFor(uint entityId) => ref _components[_entityIdIndexMap[entityId]];

    public void RemoveFor(uint entityId, out IComponent component)
    {
        int index = _entityIdIndexMap[entityId];
        component = _components[index];
        IComponent[] newComponentsArray = new IComponent[_components.Length - 1];

        Array.Copy(_components, newComponentsArray, index);

        if (index != _components.Length - 1)
        {
            Array.Copy(_components, index + 1, newComponentsArray, index, _components.Length - 1 - index);
        }

        _components = newComponentsArray;
        _entityIdIndexMap.Remove(entityId);
    }
}

public class ComponentPool<T> : ComponentPool
{
    //private readonly Dictionary<uint, int> _entityIdIndexMap = new();
    protected new T[] _components = Array.Empty<T>();

    public new IEnumerable<T> Components => Array.AsReadOnly(_components);

    public void Add(in T component, uint entityId)
    {
        int oldSize = _components.Length;
        Array.Resize(ref _components, oldSize + 1);
        _components[oldSize] = component;       // oldSize is exactly _components.Length (the new size) - 1, which is the last entry of the array
        _entityIdIndexMap.Add(entityId, oldSize);
    }

    public new ref T GetFor(uint entityId)
    {
        if (!_entityIdIndexMap.ContainsKey(entityId))
        {
            throw new AccessViolationException($"There is no Component {typeof(T).Name} for Entity {entityId}");
        }

        return ref _components[_entityIdIndexMap[entityId]];
    }

    public bool TryGetComponent(uint entityId, out T? component)
    {
        if (!_entityIdIndexMap.ContainsKey(entityId))
        {
            component = default;
            return false;
        }

        component = _components[_entityIdIndexMap[entityId]];
        return true;
    }

    public bool HasComponent(uint entityId) => _entityIdIndexMap.ContainsKey(entityId);

    public void RemoveFor(uint entityId, out T component)
    {
        if (!_entityIdIndexMap.ContainsKey(entityId))
            throw new AccessViolationException($"There is no Component {typeof(T).Name} for Entity {entityId}");

        int index = _entityIdIndexMap[entityId];
        component = _components[index];
        T[] newComponentsArray = new T[_components.Length - 1];

        Array.Copy(_components, newComponentsArray, index);

        if (index != _components.Length - 1)
        {
            Array.Copy(_components, index + 1, newComponentsArray, index, _components.Length - 1 - index);
        }

        _components = newComponentsArray;
        _entityIdIndexMap.Remove(entityId);
    }
}
