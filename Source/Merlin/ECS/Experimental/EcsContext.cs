using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.ECS.Experimental;

public class EcsContext
{
    private readonly Dictionary<Type, ComponentPool> _componentPools = new();
    private Entity[] _entities = Array.Empty<Entity>();

    public ref Entity CreateEntity()
    {
        int oldSize = _entities.Length;
        Array.Resize(ref _entities, oldSize + 1);
        _entities[oldSize] = new Entity((uint)oldSize, this);       // oldSize is exactly _components.Length (the new size) - 1, which is the last entry of the array
        return ref _entities[oldSize];
    }

    public void DestroyEntity(uint entityId)
    {
        int index = (int)entityId;
        Entity[] newEntitiesArray = new Entity[_entities.Length - 1];

        Array.Copy(_entities, newEntitiesArray, index);

        if (index != _entities.Length - 1)
        {
            Array.Copy(_entities, index + 1, newEntitiesArray, index, _entities.Length - 1 - index);
        }

        _entities = newEntitiesArray;
    }

    public ref Entity GetEntity(uint entityId)
    {
        return ref _entities[entityId];
    }

    public bool TryGetEntity(uint entityId, out Entity entity)
    {
        throw new NotImplementedException();
    }

    public ComponentPool<T> GetPool<T>()
    {
        var type = typeof(T);

        if (!_componentPools.TryGetValue(key: type, out var pool))
        {
            pool = new ComponentPool<T>();
            _componentPools.Add(key: type, pool);
        }

        return pool as ComponentPool<T> ?? throw new InvalidCastException($"There was no ComponentPool<{typeof(T).Name}> for Type {typeof(T).Name}");
    }
}
