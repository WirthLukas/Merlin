using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Merlin.EC;

public class Entity
{
    private readonly Dictionary<Type, object> _components = new();
    private readonly List<IUpdatable> _updateComponents = new();
    private readonly List<IDrawable> _drawingComponents = new();

    public void Update()
    {
        foreach (var component in _updateComponents)
        {
            component.Update();
        }
    }

    public void Draw()
    {
        foreach (var component in _drawingComponents)
        {
            component.Draw();
        }
    }

    public T AddComponent<T>([DisallowNull] T component)
    {
        if (component is null) throw new ArgumentNullException(nameof(component));

        if (!_components.ContainsKey(typeof(T)))
        {
            _components[typeof(T)] = component;

            if (component is IInitializable initializableComponent)
            {
                initializableComponent.Initialize(this);
            }

            if (component is IUpdatable updatableComponent)
            {
                _updateComponents.Add(updatableComponent);
            }

            if (component is IDrawable drawableComponent)
            {
                _drawingComponents.Add(drawableComponent);
            }
        }

        return component;
    }

    public T AddComponent<T>() where T : new() => AddComponent(new T());

    public T? GetComponent<T>()
    {
        if (_components.ContainsKey(typeof(T)))
            return (T) _components[typeof(T)];

        return default;
    }

    public bool HasComponent<T>() => _components.ContainsKey(typeof(T));

    public T? RemoveComponent<T>()
    {
        if (!_components.ContainsKey(typeof(T))) return default;

        T component = (T) _components[typeof(T)];
        _components.Remove(typeof(T));

        if (component is IUpdatable updatableComponent)
        {
            _updateComponents.Remove(updatableComponent);
        }

        if (component is IDrawable drawableComponent)
        {
            _drawingComponents.Remove(drawableComponent);
        }

        if (component is IDisposable disposableComponent)
        {
            disposableComponent.Dispose();
        }

        return component;
    }
}
