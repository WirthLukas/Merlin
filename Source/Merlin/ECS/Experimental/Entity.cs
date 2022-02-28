using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.ECS.Experimental;

public readonly struct Entity
{
    public uint Id { get; }
    public EcsContext Context { get; }

    public Entity(uint id, EcsContext context)
    {
        Id = id;
        Context = context;
    }

    public Entity AddComponent<T>(ref T component)
    {
        Context.GetPool<T>().Add(in component, Id);

        if (component is IOnAddedToEntity addable)
        {
            addable.OnAddedToEntity(in this);
        }

        return this;
    }

    public Entity RemoveComponent<T>(out T component)
    {
        Context.GetPool<T>().RemoveFor(Id, out component);

        if (component is IOnRemovedFromEntity removeable)
        {
            removeable.OnRemovedFromEntity(in this);
        }

        return this;
    }

    public ref T GetComponent<T>()
    {
        return ref Context.GetPool<T>().GetFor(Id);
    }

    public bool TryGetComponent<T>(out T? component) => Context.GetPool<T>().TryGetComponent(Id, out component);

    public bool HasComponent<T>() => Context.GetPool<T>().HasComponent(Id);

    public void Destroy()
    {
        Context.DestroyEntity(Id);
    }
}
